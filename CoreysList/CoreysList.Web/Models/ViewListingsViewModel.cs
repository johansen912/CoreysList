using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreysList.Entity;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreysList.Web.Models
{
    public class ViewListingsViewModel
    {
        #region Constructors

        //constructor for the model
        public ViewListingsViewModel() { }
        public ViewListingsViewModel(int cityId, int subCategoryId)
        {
            CoreysListEntities Db = new CoreysListEntities();
            this.Listings = Db.Listings.Where(l => l.CityID == cityId && l.SubCategoryID == subCategoryId).ToList();
            this.SubCatHeader = Db.SubCategories.FirstOrDefault(s => s.SubCategoryID == subCategoryId).SubCategoryName;
            this.SubCatId = subCategoryId; 
            this.CityName = Db.Cities.FirstOrDefault(c => c.CityID == cityId).CityName;
            this.CityId = cityId;
            this.TabId = 0; 
        }

        #endregion

        #region Properties

        public string SubCatHeader { get; set; }
        public string CityName { get; set; }
        public List<Listing> Listings { get; set; }
        public bool HasImageFilter { get; set; }
        public bool PostedTodayFilter { get; set; }
        public int TabId { get; set; }
        public int CityId { get; set; }
        public int SubCatId { get; set; }
        #endregion
    }
}