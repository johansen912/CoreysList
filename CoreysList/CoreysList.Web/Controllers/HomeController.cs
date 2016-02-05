using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.Mvc;
using CoreysList.Web.Models;

namespace CoreysList.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            int userId;
            if( Session["UserId"] != null)
            {
                userId = Convert.ToInt32(Session["UserId"]);
            }
            if (Request.Cookies["cityId"] != null)
            {
                int cityId = Convert.ToInt32(Request.Cookies["cityId"].Value);
                HomeIndexViewModel model = new HomeIndexViewModel(cityId);
                return View("Index", model);
            }
            else
            {
                LocatorViewModel model = new LocatorViewModel();
                return View("Locator", model);
            }
        }

        public ActionResult Locator()
        {
            LocatorViewModel model = new LocatorViewModel();
            return View("Locator", model);
        }


        public ActionResult StateSelector( int stateId)
        {
            StateSelectorViewModel model = new StateSelectorViewModel(stateId);
            return View("StateSelector", model);
        }

    }
}
