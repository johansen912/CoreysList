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
    public class EditListingViewModel
    {
        #region Constructors

        //constructor for the model
        public EditListingViewModel(){}

        public EditListingViewModel(int listingId)
        {
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                CoreysListEntities Db = new CoreysListEntities();

                this.States = new List<SelectListItem>();
                this.Categories = new List<SelectListItem>();
                this.Subcategories = new List<SelectListItem>();
                this.Cities = new List<SelectListItem>();

                if (listingId == -1)
                {
                    this.Listing = new Listing();
                    this.Listing.ListingID = -1;

                    List<State> states = Db.States.OrderBy(c => c.StateName).ToList();
                    SelectListItem stateItem1 = new SelectListItem { Text = "Select a State", Value = "" };
                    this.States.Add(stateItem1);

                    foreach (State state in states)
                    {
                        SelectListItem stateItem = new SelectListItem { Text = state.StateName, Value = state.StateID.ToString()};
                        this.States.Add(stateItem);
                    }

                    List<Category> categories = Db.Categories.OrderBy(c => c.CategoryName).ToList();
                    SelectListItem categoryItem1 = new SelectListItem { Text = "Select a Category", Value = "" };
                    this.Categories.Add(categoryItem1);

                    foreach (Category category in categories)
                    {
                        SelectListItem categoryItem = new SelectListItem { Text = category.CategoryName, Value = category.CategoryID.ToString()};
                        this.Categories.Add(categoryItem);
                    }

                }
                else
                {

                    this.Listing = Db.Listings.FirstOrDefault(l => l.ListingID == listingId);

                    List<State> states = Db.States.OrderBy(c => c.StateName).ToList();

                    foreach( State state in states)
                    {
                        bool selected = Listing.City.StateID == state.StateID ? true : false;
                        SelectListItem stateItem = new SelectListItem { Text = state.StateName, Value = state.StateID.ToString(), Selected = selected };
                        this.States.Add(stateItem);
                    }
                    List<City> cities = Db.Cities.Where(c => c.StateID == this.Listing.City.StateID).OrderBy(c => c.CityName).ToList();
                    foreach(City city in cities)
                    {
                        bool selected = Listing.CityID == city.CityID ? true : false;
                        SelectListItem cityItem = new SelectListItem { Text = city.CityName, Value = city.CityID.ToString() , Selected = selected };
                        this.Cities.Add(cityItem);
                    }
                    List<Category> categories = Db.Categories.OrderBy(c => c.CategoryName).ToList();

                    foreach( Category category in categories)
                    {
                        bool selected = Listing.SubCategory.CategoryID == category.CategoryID ? true : false;
                        SelectListItem categoryItem = new SelectListItem { Text = category.CategoryName, Value = category.CategoryID.ToString(), Selected = selected };
                        this.Categories.Add(categoryItem);
                    }
                    List<SubCategory> subcategories = Db.SubCategories.Where(s => s.CategoryID == this.Listing.SubCategory.CategoryID).OrderBy(s => s.SubCategoryName).ToList();
                    foreach(SubCategory subcategory in subcategories)
                    {
                        bool selected = Listing.SubCategoryID == subcategory.SubCategoryID ? true : false;
                        SelectListItem subcategoryItem  = new SelectListItem { Text = subcategory.SubCategoryName, Value = subcategory.SubCategoryID.ToString(), Selected = selected };
                        this.Subcategories.Add(subcategoryItem);
                    }
                }
            }
           

        }

        #endregion

        #region Properties

        public Listing Listing { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> Subcategories { get; set; }
        public List<SelectListItem> Categories { get; set; }

        #endregion

    }
}