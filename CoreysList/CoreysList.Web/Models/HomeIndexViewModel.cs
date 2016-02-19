using System;
using System.Collections.Generic;
using System.Linq;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class HomeIndexViewModel
    {
        #region Constructors

        public HomeIndexViewModel() 
        { 
        }

        // constructor for the model
        public HomeIndexViewModel(int cityId)
        {
            CoreysListEntities db = new CoreysListEntities();
            
            // populate properties for page layout
            this.Categories1 = db.Categories.Where(c => c.CategoryID <= 3).OrderBy(c => c.CategoryName).ToList();
            this.Categories2 = db.Categories.Where(c => c.CategoryID > 3 && c.CategoryID <= 6).OrderBy(c => c.CategoryName).ToList();
            this.Categories3 = db.Categories.Where(c => c.CategoryID > 6).OrderBy(c => c.CategoryName).ToList();
            this.States = db.States.OrderBy(s => s.StateName).ToList();
            this.TopCities = db.Cities.Where(c => c.MajorCity == true).OrderBy(c => c.CityName).ToList();
            this.CityName = db.Cities.Where(c => c.CityID == cityId).FirstOrDefault().CityName;
            this.CityId = cityId;
            this.SearchTerm = "";
        }

        #endregion

        #region Properties

        public List<Category> Categories1 { get; set; }

        public List<Category> Categories2 { get; set; }

        public List<Category> Categories3 { get; set; }

        public List<State> States { get; set; }

        public List<City> TopCities { get; set; }

        public string SearchTerm { get; set;  }

        public string CityName { get; set; }

        public int? CityId { get; set; }

        #endregion
    }
}