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
    public class DisplayListingViewModel
    {
         #region Constructors

        //constructor for the model
        public DisplayListingViewModel() { }
        public DisplayListingViewModel(int listingId)
        {
            CoreysListEntities Db = new CoreysListEntities();
            ListingToDisplay = Db.Listings.FirstOrDefault(l => l.ListingID == listingId);
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