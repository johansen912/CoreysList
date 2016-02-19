using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CoreysList.Entity;
using CoreysList.Web.Models;

namespace CoreysList.Web.Controllers
{
    public class AccountsController : Controller
    {
        private CoreysListEntities db = new CoreysListEntities();

        // GET: /Accounts/
        public ActionResult Index()
        {
            // Check to insure user in logged in
            if (Session["UserId"] != null)
            {
                // if so send them to the home page
                UserHomeViewModel userHomeModel = new UserHomeViewModel();
                return View("UserHome", userHomeModel);
            }
            else
            {
                // else send them to login page
                LoginViewModel model = new LoginViewModel();
                return View("Index", model);
            }  
        }

        // when user submits login form
        public ActionResult Log_In(LoginViewModel model)
        {
            // search for the users email in the database
            User user = db.Users.FirstOrDefault(c => c.Email == model.LoginEmail);

            // if the user is found
            if (user != null)
            {
                // check to see if passwords match
                if (user.Password == model.LoginPassword)
                {
                    // set the user id and email into session variables
                    Session["UserId"] = user.UserID;
                    Session["UserEmail"] = user.Email;

                    // send user to homepage
                    UserHomeViewModel userHomeModel = new UserHomeViewModel();
                    return View("UserHome", userHomeModel);
                }
                else
                {
                    // return to login screen and notify user of  invalid password
                    model.LoginErrorMessage = "Invalid Password";
                    return View("Index", model);
                }
            }
            else
            {
                // return to login screen and notify user that email was not found
                model.LoginErrorMessage = "Email not found";
                return View("Index", model);
            }
        }

        // when the user submits signup 
        public ActionResult Create_Account(LoginViewModel model)
        {
            // establish connection to database
            CoreysListEntities db = new CoreysListEntities();

            try
            {
                // Checking to see if the email entered already exist in database
                User testUser = db.Users.FirstOrDefault(u => u.Email == model.Email);

                // if not create and add new user
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

                    // save the new user to the database
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    // set the new users information in session variables
                    Session["UserId"] = newUser.UserID;
                    Session["UserEmail"] = newUser.Email;

                    // send user to homepage
                    UserHomeViewModel userHomeModel = new UserHomeViewModel();
                    return View("UserHome", userHomeModel);
                }
                else
                {
                    // else return error message informing user that email already is registered
                    model.CreateAccountErrorMessage = "Email already in use";
                    return View("Index", model);
                }
            }  
            catch (Exception e)
            {
                // exception connecting to database
                string error = e.Message;
                return View("Index", model);
            }
        }

        // Action when homepage is called
        public ActionResult UserHome(int tabId = 0)
        {
            // if user is not logged in return them to login page
            if (Session["UserId"] == null)
            {
                LoginViewModel loginModel = new LoginViewModel();
                return View("Index", loginModel);
            }

            // send them to their home page
            UserHomeViewModel userHomeModel = new UserHomeViewModel();

            // tabId used to keep track of the current Jquery ui tab the user is on
            userHomeModel.TabId = tabId;
            return View("UserHome", userHomeModel);
        }

        // called when user clicks logout 
         public ActionResult LogOut()
        {
            // clear their session variables return to login page
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        // loads the initial account update page
        [HttpGet]
         public ActionResult AccountUpdate()
         {
            // gets the current users info using userid stored in session and returns the user
             CoreysListEntities db = new CoreysListEntities();
             int userId = Convert.ToInt32(Session["UserId"]);
             User user = db.Users.FirstOrDefault(u => u.UserID == userId);
             return View("AccountUpdate", user); 
         }

        // called when the user submits account update page
        [HttpPost]
         public ActionResult AccountUpdate(User updatedUserInfo)
         {
            // get the user from the database and update thier information
             CoreysListEntities db = new CoreysListEntities();
             User user = db.Users.FirstOrDefault(u => u.UserID == updatedUserInfo.UserID);
             user.PhoneNum = updatedUserInfo.PhoneNum;
             user.FirstName = updatedUserInfo.FirstName;
             user.LastName = updatedUserInfo.LastName;
             user.Password = updatedUserInfo.Password;

            // save the changes made to the user
             db.SaveChanges();

            // send the user to their homepage
             UserHomeViewModel userHomeModel = new UserHomeViewModel();
             return View("UserHome", userHomeModel);
         }

        // called when the user wants to activate or deactivate a listing
        public ActionResult ListingActivation(int listingId)
        {
            // establish connection to database
            CoreysListEntities db = new CoreysListEntities();

            // create variable to get the current activation status
            string activationStatus;

            // get the selected listing to update
            Listing listing = db.Listings.FirstOrDefault(l => l.ListingID == listingId);

            try
            {
                // change the active listing status
                listing.IsActive = listing.IsActive ? false : true;

                // save the changes 
                db.SaveChanges();

                // get the new activation status
                activationStatus = listing.IsActive ? "active" : "notActive";

                // return json with the new status
                return Json(new { Success = true, ActivationStatus = activationStatus });
            }
            catch (Exception e)
            {
                // if there was a problem with the return send error
                return Json(new { Success = false, Error = "Activation or Deactivation Failed: " + e.Message });
            }
        }

        // Called when a user selects category in cascading select list to populate subcategory select list
        public ActionResult GetSubCategories(int categoryId)
        {
            CoreysListEntities db = new CoreysListEntities();
            try
            {
                // get a list of subcategories where category Id matches selected category ID
                List<SubCategory> subcategories = db.SubCategories.Where(s => s.CategoryID == categoryId)
                                                                  .OrderBy(s => s.SubCategoryName).ToList();

                // create a string builder and add the new markup for subcategory selectlist
                StringBuilder sb = new StringBuilder();

                // append first default select list item
                sb.Append("<option value = '0'>Please select an item</option>");

                // append a new selectlist item for each subcategory
                foreach (SubCategory subcategory in subcategories)
                {
                    sb.Append("<option value = '" + subcategory.SubCategoryID.ToString() + "'>" + subcategory.SubCategoryName + "</option>");
                }

                // return the stringbuilders appended string in json
                return Json(new { Success = true, SelectOptionsHtml = sb.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Error = "Activation or Deactivation Failed: " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // called to populate sub select list for states
        public ActionResult GetCities(int stateId)
        {
            CoreysListEntities db = new CoreysListEntities();
            try
            {
                // get a list of cities where state id matches
                List<City> cities = db.Cities.Where(s => s.StateID == stateId)
                                             .OrderBy(s => s.CityName).ToList();

                // create string builder to hold markup for new select list
                StringBuilder sb = new StringBuilder();

                // add default selectlist item to stringbuilder
                sb.Append("<option value = '0'>Please select an item</option>");

                // for each city with matching state id append markup to stringbuilder
                foreach (City city in cities)
                {
                    sb.Append("<option value = '" + city.CityID.ToString() + "'>" + city.CityName + "</option>");
                }

                // return new stringbuilder appended content using json
                return Json(new { Success = true, SelectOptionsHtml = sb.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Error = "Activation or Deactivation Failed: " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // when user selects listing to edit 
        [HttpGet]
        public ActionResult EditListing(int listingId)
        {
            // return partial view and editlistingmodel with selected listings id
            return View("_EditListing", new EditListingViewModel(listingId));
        }

        // when the user submits changes to listing
        [HttpPost]
         public ActionResult EditListing(EditListingViewModel model)
        {
             CoreysListEntities db = new CoreysListEntities();
             try
             {
                 // if the listing ID is -1 then it is a new listing
                 if (model.Listing.ListingID == -1)
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

                    db.Listings.Add(listing);
                    db.SaveChanges();
                 }
                 else
                 {
                     // get the listing being edited
                     Listing listing = db.Listings.FirstOrDefault(l => l.ListingID == model.Listing.ListingID);

                     // update all the fields and save 
                     listing.CityID = model.Listing.CityID;
                     listing.UserID = Convert.ToInt32(Session["UserId"]);
                     listing.SubCategoryID = model.Listing.SubCategoryID;
                     listing.Headline = model.Listing.Headline;
                     listing.Location = model.Listing.Location;
                     listing.Description = model.Listing.Description;
                     listing.Price = model.Listing.Price;
                     listing.ModifiedBy = listing.User.Email;
                     listing.ModifiedDate = DateTime.Now;

                     db.SaveChanges();
                 }
             }
             catch (Exception e)
             {
                 throw e;
             }

            // return success and handle navigation in jquery
             return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        // called when the user clicks the images link for the listing
        public ActionResult EditImages(int listingId)
        {
            // return the editlistingsImages partial view
            EditListingImagesViewModel model = new EditListingImagesViewModel(listingId);
            return View("_EditListingImages", model);
        }
    }
}
