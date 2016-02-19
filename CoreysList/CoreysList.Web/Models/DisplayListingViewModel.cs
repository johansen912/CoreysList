using System.Linq;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class DisplayListingViewModel
    {
         #region Constructors

        // constructor for the model
        public DisplayListingViewModel() 
        { 
        }

        public DisplayListingViewModel(int listingId)
        {
            CoreysListEntities db = new CoreysListEntities();

            // populate the listing being displayed 
            ListingToDisplay = db.Listings.FirstOrDefault(l => l.ListingID == listingId);
            SubCatId = ListingToDisplay.SubCategoryID;
            CityId = ListingToDisplay.CityID;
        }

        #endregion

        #region Properties

        public Listing ListingToDisplay { get; set; }

        public int SubCatId { get; set; }

        public int CityId { get; set; }

        #endregion
    }
}