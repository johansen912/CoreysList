using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CoreysList.Entity;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreysList.Web.Models
{
    public class EditListingImagesViewModel
    {
        #region Constructors

        //constructor for the model
        public EditListingImagesViewModel() { }

        public EditListingImagesViewModel(int listingId)
        {
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                CoreysListEntities Db = new CoreysListEntities();
                ImageIds = Db.Images.Where(i => i.ListingID == listingId).Select(i => i.ImageID).ToList();
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