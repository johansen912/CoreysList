using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Text;
using CoreysList.Web.Models;
using CoreysList.Entity;

namespace CoreysList.Web.Controllers
{
    public class AccountsController : Controller
    {
        CoreysListEntities Db = new CoreysListEntities();
        //
        // GET: /Accounts/

        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                UserHomeViewModel userHomeModel = new UserHomeViewModel();
                return View("UserHome", userHomeModel);
            }
            else
            {
                LoginViewModel model = new LoginViewModel();
                return View("Index", model);
            }  
        }

        public ActionResult Log_In( LoginViewModel model )
        {
            User user = Db.Users.FirstOrDefault(c => c.Email == model.LoginEmail);
            if(user != null )
            {
                if( user.Password == model.LoginPassword)
                {
                    Session["UserId"] = user.UserID;
                    Session["UserEmail"] = user.Email;
                    UserHomeViewModel userHomeModel = new UserHomeViewModel();
                    return View("UserHome", userHomeModel);
                    
                }
                else
                {
                    model.LoginErrorMessage = "Invalid Password";
                    return View("Index", model);
                }
            }
            else
            {
                model.LoginErrorMessage = "Email not found";
                return View("Index", model);
            }
        }

        public ActionResult Create_Account(LoginViewModel model)
        {
            CoreysListEntities Db = new CoreysListEntities();
            try
            {
                //Checking to see if the email entered already exist in database
                User testUser = Db.Users.FirstOrDefault(u => u.Email == model.Email);
                //if not create and add new user
                if (testUser == null)
                {
                    User newUser = new User();
                    newUser.FirstName = model.FirstName;
                    newUser.LastName = model.LastName;
                    newUser.Email = model.Email;
                    newUser.PhoneNum = Convert.ToString(model.PhoneNumber.Replace("-", ""));
                    newUser.Password = model.Password;
                    newUser.CreatedBy = "Corey";
                    newUser.CreatedDate = DateTime.Now;

                    Db.Users.Add(newUser);
                    Db.SaveChanges();

                    Session["UserId"] = newUser.UserID;
                    Session["UserEmail"] = newUser.Email;
                    UserHomeViewModel userHomeModel = new UserHomeViewModel();
                    return View("UserHome", userHomeModel);
                }
                    //else return error message
                else
                {
                    model.CreateAccountErrorMessage = "Email already in use";
                    return View("Index", model);
                }
            }  
            catch(Exception e)
            {
                string error = e.Message;
                return View("Index", model);
            }
        }

        public ActionResult UserHome(int tabId = 0)
        {
            if( Session["UserId"] == null)
            {
                LoginViewModel loginModel = new LoginViewModel();
                return View("Index", loginModel);
            }
            UserHomeViewModel userHomeModel = new UserHomeViewModel();
            userHomeModel.TabId = tabId;
            return View("UserHome", userHomeModel);
        }

         public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
         public ActionResult AccountUpdate()
         {
             CoreysListEntities Db = new CoreysListEntities();
             int userId = Convert.ToInt32(Session["UserId"]);
             User user = Db.Users.FirstOrDefault(u => u.UserID == userId);
             return View("AccountUpdate", user); 
         }

        [HttpPost]
         public ActionResult AccountUpdate(User updatedUserInfo)
         {
             CoreysListEntities Db = new CoreysListEntities();
             User user = Db.Users.FirstOrDefault(u => u.UserID == updatedUserInfo.UserID);
             user.PhoneNum = updatedUserInfo.PhoneNum;
             user.FirstName = updatedUserInfo.FirstName;
             user.LastName = updatedUserInfo.LastName;
             user.Password = updatedUserInfo.Password;
             Db.SaveChanges();
             UserHomeViewModel userHomeModel = new UserHomeViewModel();
             return View("UserHome", userHomeModel);
         }

        public ActionResult ListingActivation( int listingId )
        {
            CoreysListEntities Db = new CoreysListEntities();
            string activationStatus;
            Listing listing = Db.Listings.FirstOrDefault(l => l.ListingID == listingId);
            try
            {
                listing.IsActive = listing.IsActive ? false : true;

                Db.SaveChanges();
                activationStatus = listing.IsActive ? "active" : "notActive";
                return Json(new { Success = true, ActivationStatus = activationStatus });
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Error = "Activation or Deactivation Failed: " + e.Message });
            }

        }

        public ActionResult GetSubCategories( int categoryId)
        {
            CoreysListEntities Db = new CoreysListEntities();
            try
            {
                List<SubCategory> subcategories = Db.SubCategories.Where(s => s.CategoryID == categoryId)
                                                                  .OrderBy(s => s.SubCategoryName).ToList();
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value = '0'>Please select an item</option>" );
                foreach(SubCategory subcategory in subcategories)
                {
                    sb.Append("<option value = '" + subcategory.SubCategoryID.ToString() + "'>" +  subcategory.SubCategoryName +"</option>");
                }


                return Json(new { Success = true, SelectOptionsHtml = sb.ToString()}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Error = "Activation or Deactivation Failed: " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCities(int stateId)
        {
            CoreysListEntities Db = new CoreysListEntities();
            try
            {
                List<City> cities = Db.Cities.Where(s => s.StateID == stateId)
                                             .OrderBy(s => s.CityName).ToList();
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value = '0'>Please select an item</option>");
                foreach (City city in cities)
                {
                    sb.Append("<option value = '" + city.CityID.ToString() + "'>" + city.CityName + "</option>");
                }


                return Json(new { Success = true, SelectOptionsHtml = sb.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Error = "Activation or Deactivation Failed: " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult EditListing(int listingId)
        {
            return View("_EditListing", new EditListingViewModel(listingId));
        }

        [HttpPost]
         public ActionResult EditListing(EditListingViewModel model)
        {
             CoreysListEntities Db = new CoreysListEntities();
             try
             {
                 if( model.Listing.ListingID == -1)
                 {
                     // add new listing
                     Listing listing = new Listing();
                     listing.CityID = model.Listing.CityID;
                     listing.UserID = Convert.ToInt32(Session["UserId"]);
                     listing.SubCategoryID = model.Listing.SubCategoryID;
                     listing.Headline = model.Listing.Headline;
                     listing.Location = model.Listing.Location;
                     listing.Description = model.Listing.Description;
                     listing.Price = model.Listing.Price;
                     listing.CreatedBy = "corey";
                     listing.CreatedDate = DateTime.Now;

                    Db.Listings.Add(listing);
                    Db.SaveChanges();
 
                 }
                 else
                 {
                     Listing listing = Db.Listings.FirstOrDefault( l => l.ListingID == model.Listing.ListingID);
                     listing.CityID = model.Listing.CityID;
                     listing.UserID = Convert.ToInt32(Session["UserId"]);
                     listing.SubCategoryID = model.Listing.SubCategoryID;
                     listing.Headline = model.Listing.Headline;
                     listing.Location = model.Listing.Location;
                     listing.Description = model.Listing.Description;
                     listing.Price = model.Listing.Price;
                     listing.ModifiedBy = listing.User.Email;
                     listing.ModifiedDate = DateTime.Now;

                     Db.SaveChanges();
                 }
             }
             catch (Exception e)
             {
                 throw (e);
             }

             //EditListingImagesViewModel editListingImagesViewModel = new EditListingImagesViewModel(model.Listing.ListingID);
             //return View("_EditListing", model);
             return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditImages(int listingId)
        {
            EditListingImagesViewModel model = new EditListingImagesViewModel(listingId);
            return View("_EditListingImages", model);
        }
    }
}
