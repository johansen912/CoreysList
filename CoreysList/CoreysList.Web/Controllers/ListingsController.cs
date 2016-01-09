using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreysList.Web.Models;
using CoreysList.Entity;

namespace CoreysList.Web.Controllers
{
    public class ListingsController : Controller
    {
        //
        // GET: /Listings/

        public ActionResult Index(int subcategoryId)
        {
            if (Request.Cookies["cityId"] != null)
            {
                int cityId = Convert.ToInt32(Request.Cookies["cityId"].Value);
                ViewListingsViewModel model = new ViewListingsViewModel(cityId, subcategoryId);
                return View("ViewListings", model);
            }
            else
            {
                LocatorViewModel locatorModel = new LocatorViewModel();
                Response.Redirect("/Home/Locator");
                return View();
            }
        }


        public ActionResult ApplyListingsFilters(ViewListingsViewModel model)
        {
            CoreysListEntities Db = new CoreysListEntities();

            //check filters
            if (model.HasImageFilter == true && model.PostedTodayFilter == true)
            {
                model.Listings = Db.Listings.Where(l => l.CreatedDate == DateTime.Now && l.Images.Count() > 0 && l.CityID == model.CityId && l.SubCategoryID == model.SubCatId).ToList();
            }
            else if (model.PostedTodayFilter == true)
            {
                model.Listings = Db.Listings.Where(l => l.CreatedDate == DateTime.Now && l.CityID == model.CityId && l.SubCategoryID == model.SubCatId).ToList();
            }
            else if (model.HasImageFilter == true)
            {
                model.Listings = Db.Listings.Where(l => l.Images.Count() > 0 && l.CityID == model.CityId && l.SubCategoryID == model.SubCatId).ToList();
            }
            else
            {
                model.Listings = Db.Listings.Where(l => l.CityID == model.CityId && l.SubCategoryID == model.SubCatId).ToList();
            }

            return View("ViewListings", model);
        }

        public ActionResult DisplayListing(int listingId)
        {
            DisplayListingViewModel model = new DisplayListingViewModel(listingId);
            return View("DisplayListing", model);
        }
    }
}
