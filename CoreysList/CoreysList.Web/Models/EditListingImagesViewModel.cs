using System.Collections.Generic;
using System.Linq;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class EditListingImagesViewModel
    {
        #region Constructors

        // constructor for the model
        public EditListingImagesViewModel() 
        {
        }

        public EditListingImagesViewModel(int listingId)
        {
            // check to make sure user is logged in
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                CoreysListEntities db = new CoreysListEntities();

                // retrieve the image ids matching the listing id
                ImageIds = db.Images.Where(i => i.ListingID == listingId).Select(i => i.ImageID).ToList();
                ListingId = listingId;
            }
        }

        #endregion

        #region Properties

        public List<int> ImageIds { get; set; }

        public int ListingId { get; set; }

        #endregion
    }
}