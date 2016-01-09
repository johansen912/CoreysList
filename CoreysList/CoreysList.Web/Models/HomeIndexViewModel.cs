using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class HomeIndexViewModel
    {
        #region Constructors

        //constructor for the model
        public HomeIndexViewModel(int cityId)
        {
            CoreysListEntities Db = new CoreysListEntities();
            
            this.Categories1 = Db.Categories.Where(c => c.CategoryID <= 3).OrderBy(c => c.CategoryName).ToList();
            this.Categories2 = Db.Categories.Where(c => c.CategoryID > 3 && c.CategoryID <= 6).OrderBy(c => c.CategoryName).ToList();
            this.Categories3 = Db.Categories.Where(c => c.CategoryID > 6 ).OrderBy(c => c.CategoryName).ToList();
            this.States = Db.States.OrderBy(s => s.StateName).ToList();
            this.TopCities = Db.Cities.Where(c => c.MajorCity == true).OrderBy(c => c.CityName).ToList();
            this.CityName = Db.Cities.Where(c => c.CityID == cityId).FirstOrDefault().CityName;
            this.CityId = cityId;
        }

        #endregion

        #region Properties

        // creates a list of strings and also its get and set method "Property"
        public List<Category> Categories1 { get; set; }
        public List<Category> Categories2 { get; set; }
        public List<Category> Categories3 { get; set; }
        public List<State> States { get; set; }
        public List<City> TopCities { get; set; }
        public string CityName { get; set; }
        public int? CityId { get; set; }
        #endregion
    }
}