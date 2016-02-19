using System;
using System.Web.Mvc;
using CoreysList.Web.Models;

namespace CoreysList.Web.Controllers
{
    public class HomeController : Controller
    {
        // First action called when application runs
        public ActionResult Index()
        {
            // create variable to keep track of the user ID
            int userId;

            // check to see if the user id is stored in session
            if (Session["UserId"] != null)
            {
                // assign logged in users id to variable
                userId = Convert.ToInt32(Session["UserId"]);
            }

            // check to see if the users cityId is stored as a cookie
            if (Request.Cookies["cityId"] != null)
            {
                // if so get the cookie and send the user to homescreen for that city
                int cityId = Convert.ToInt32(Request.Cookies["cityId"].Value);
                HomeIndexViewModel model = new HomeIndexViewModel(cityId);
                return View("Index", model);
            }
            else
            {
                // send the user to locator page to select their current city
                LocatorViewModel model = new LocatorViewModel();
                return View("Locator", model);
            }
        }

        // sends the user to the locator page to select their city
        public ActionResult Locator()
        {
            LocatorViewModel model = new LocatorViewModel();
            return View("Locator", model);
        }

        // sends the user to select their current state
        public ActionResult StateSelector(int stateId)
        {
            StateSelectorViewModel model = new StateSelectorViewModel(stateId);
            return View("StateSelector", model);
        }

        // when the user submits a searchbar post
        public ActionResult SearchBarPost(HomeIndexViewModel model)
        {
            // send the user to the listings controller action displaysearchresults with the search term as a parameter
            return RedirectToAction("DisplaySearchResults", "Listings", new { searchTerm = model.SearchTerm });
        }
    }
}
