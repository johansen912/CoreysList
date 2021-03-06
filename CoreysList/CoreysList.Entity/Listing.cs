//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoreysList.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Listing
    {
        public Listing()
        {
            this.Images = new HashSet<Image>();
        }
    
        public int ListingID { get; set; }
        public int UserID { get; set; }
        public int CityID { get; set; }
        public int SubCategoryID { get; set; }
        public string Headline { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    
        public virtual SubCategory SubCategory { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual User User { get; set; }
    }
}
