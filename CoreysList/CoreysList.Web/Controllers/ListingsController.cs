using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CoreysList.Entity;
using CoreysList.Web.Models;

namespace CoreysList.Web.Controllers
{
    public class ListingsController : Controller
    {
        // called when a user clicks a subcategory id to view listings
        public ActionResult Index(int subcategoryId)
        {
            // check to make sure city id cookie is populated
            if (Request.Cookies["cityId"] != null)
            {
                // return the view listings with the city and subcategory id
                int cityId = Convert.ToInt32(Request.Cookies["cityId"].Value);
                ViewListingsViewModel model = new ViewListingsViewModel(cityId, subcategoryId);
                return View("ViewListings", model);
            }
            else
            {
                // there is no selected city id so send user to locator to chose a city
                LocatorViewModel locatorModel = new LocatorViewModel();
                Response.Redirect("/Home/Locator");
                return View();
            }
        }

        // Called when the user submits the apply filters form to the view listings page
        public ActionResult ApplyListingsFilters(ViewListingsViewModel model)
        {
            CoreysListEntities db = new CoreysListEntities();

            // check filters
            if (model.HasImageFilter == true && model.PostedTodayFilter == true)
            {
                model.Listings = db.Listings.Where(l => l.CreatedDate == DateTime.Now && l.Images.Count() > 0 && l.CityID == model.CityId && l.SubCategoryID == model.SubCatId).ToList();
            }
            else if (model.PostedTodayFilter == true)
            {
                model.Listings = db.Listings.Where(l => l.CreatedDate == DateTime.Now && l.CityID == model.CityId && l.SubCategoryID == model.SubCatId).ToList();
            }
            else if (model.HasImageFilter == true)
            {
                model.Listings = db.Listings.Where(l => l.Images.Count() > 0 && l.CityID == model.CityId && l.SubCategoryID == model.SubCatId).ToList();
            }
            else
            {
                model.Listings = db.Listings.Where(l => l.CityID == model.CityId && l.SubCategoryID == model.SubCatId).ToList();
            }

            // return the filtered listings
            return View("ViewListings", model);
        }

        // Returns the display listing view for the selected listing
        public ActionResult DisplayListing(int listingId)
        {
            DisplayListingViewModel model = new DisplayListingViewModel(listingId);
            return View("DisplayListing", model);
        }
        
        // Returns the DisplaySearchResults view with listings that match search term 
        public ActionResult DisplaySearchResults(string searchTerm)
        {
            DisplaySearchResultsViewModel searchModel = new DisplaySearchResultsViewModel(searchTerm);
            return View("DisplaySearchResults", searchModel);
        }

        // Used for the cascading select list in the display search results view filters
        [HttpPost]
        public ActionResult GetCitiesByStateId(int stateid)
        {
            CoreysListEntities db = new CoreysListEntities();

            // retreive the cities matching the state id
            List<City> cities = new List<City>();
            cities = db.Cities.Where(m => m.StateID == stateid).ToList();

            // create a stringbuilder and append the mark up for each city to add selectlist items
            StringBuilder sb = new StringBuilder();
            sb.Append("<option value = '0'>-- Select City --</option>");
            foreach (City city in cities)
            {
                sb.Append("<option value = '" + city.CityID.ToString() + "'>" + city.CityName + "</option>");
            }

            // return the stringbuilder appended content via json
            return Json(new { Success = true, SelectOptionsHtml = sb.ToString() }, JsonRequestBehavior.AllowGet);
        }

        // When the user applys the filters to the DisplaySearchResults View
        [HttpPost]
        public ActionResult FilterSearchResults(DisplaySearchResultsViewModel model)
        {
            CoreysListEntities db = new CoreysListEntities();

            // get a count of the cities
            int cityCount = model.Cities.Count();

            // populate cities dropdown with selected state
            List<City> citiesList = db.Cities.Where(c => c.StateID == model.SelectedStateIdFilter).ToList();
            foreach (City c in citiesList)
            {
                SelectListItem newListItem = new SelectListItem();
                newListItem.Text = c.CityName;
                newListItem.Value = c.CityID.ToString();
                model.Cities.Add(newListItem);
            }

            // first pull all the listings that still match the search term 
            model.Listings = db.Listings.Where(l => l.Headline.Contains(model.SearchTerm)
                || l.Description.Contains(model.SearchTerm)).ToList();
            
            // if a city was selected
            if (model.SelectedCityIdFilter > 0)
            {
                model.Listings = model.Listings.Where(l => l.CityID == model.SelectedCityIdFilter
                                   && l.Price >= model.PriceMinFilter
                                   && l.Price <= model.PriceMaxFilter).ToList();
            } 
            else if (model.SelectedStateIdFilter > 0)
            {
                // else if state was selected but no city
                model.Listings = model.Listings.Where(l => l.City.StateID == model.SelectedStateIdFilter
                                   && l.Price >= model.PriceMinFilter
                                   && l.Price <= model.PriceMaxFilter).ToList();
            }
            else
            {
                // else just apply price range
                model.Listings = model.Listings.Where(l => l.Price >= model.PriceMinFilter
                                   && l.Price <= model.PriceMaxFilter).ToList();
            }

            // return filtered model with view
            return View("DisplaySearchResults", model);
        }
    }
}
