using System.Collections.Generic;
using System.Linq;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class ViewListingsViewModel
    {
        #region Constructors

        // constructor for the model
        public ViewListingsViewModel() 
        { 
        }

        public ViewListingsViewModel(int cityId, int subCategoryId)
        {
            // populate the properties to display the listings
            CoreysListEntities db = new CoreysListEntities();
            this.Listings = db.Listings.Where(l => l.CityID == cityId && l.SubCategoryID == subCategoryId).ToList();
            this.SubCatHeader = db.SubCategories.FirstOrDefault(s => s.SubCategoryID == subCategoryId).SubCategoryName;
            this.SubCatId = subCategoryId; 
            this.CityName = db.Cities.FirstOrDefault(c => c.CityID == cityId).CityName;
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