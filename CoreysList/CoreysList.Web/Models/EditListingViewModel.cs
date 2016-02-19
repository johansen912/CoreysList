using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class EditListingViewModel
    {
        #region Constructors

        // constructor for the model
        public EditListingViewModel()
        {
        }

        public EditListingViewModel(int listingId)
        {
            // check to make sure user is logged in
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                CoreysListEntities db = new CoreysListEntities();

                // instantiate lists of selectlistitems
                this.States = new List<SelectListItem>();
                this.Categories = new List<SelectListItem>();
                this.Subcategories = new List<SelectListItem>();
                this.Cities = new List<SelectListItem>();

                // if the listing is a new listing
                if (listingId == -1)
                {
                    // create the new listing
                    this.Listing = new Listing();
                    this.Listing.ListingID = -1;

                    // populate states property for dropdown list
                    List<State> states = db.States.OrderBy(c => c.StateName).ToList();

                    // add default value
                    SelectListItem stateItem1 = new SelectListItem { Text = "Select a State", Value = "" };
                    this.States.Add(stateItem1);

                    // convert the rest of the states into select list items and add them to list
                    foreach (State state in states)
                    {
                        SelectListItem stateItem = new SelectListItem { Text = state.StateName, Value = state.StateID.ToString() };
                        this.States.Add(stateItem);
                    }

                    // get all the categories
                    List<Category> categories = db.Categories.OrderBy(c => c.CategoryName).ToList();

                    // add default item to select list
                    SelectListItem categoryItem1 = new SelectListItem { Text = "Select a Category", Value = "" };
                    this.Categories.Add(categoryItem1);

                    // convert all the categories to select list items and add them to selectlist
                    foreach (Category category in categories)
                    {
                        SelectListItem categoryItem = new SelectListItem { Text = category.CategoryName, Value = category.CategoryID.ToString() };
                        this.Categories.Add(categoryItem);
                    }
                }
                else
                {
                    // Else listing is existing and get the current listing
                    this.Listing = db.Listings.FirstOrDefault(l => l.ListingID == listingId);

                    // get all the states
                    List<State> states = db.States.OrderBy(c => c.StateName).ToList();

                    // for each state
                    foreach (State state in states)
                    {
                        // check to see if the state is the current state for the listing
                        bool selected = Listing.City.StateID == state.StateID ? true : false;

                        // convert to select list item and add selected attribute if true
                        SelectListItem stateItem = new SelectListItem { Text = state.StateName, Value = state.StateID.ToString(), Selected = selected };
                        this.States.Add(stateItem);
                    }

                    // get all the cities where belonging to selected state
                    List<City> cities = db.Cities.Where(c => c.StateID == this.Listing.City.StateID).OrderBy(c => c.CityName).ToList();

                    foreach (City city in cities)
                    {
                        // check if the city is the current city for the listing
                        bool selected = Listing.CityID == city.CityID ? true : false;

                        // convert to selectlist item and set selected attribute
                        SelectListItem cityItem = new SelectListItem { Text = city.CityName, Value = city.CityID.ToString(), Selected = selected };
                        this.Cities.Add(cityItem);
                    }

                    // get all the categories
                    List<Category> categories = db.Categories.OrderBy(c => c.CategoryName).ToList();

                    foreach (Category category in categories)
                    {
                        // check if category is the current category for the selected listing
                        bool selected = Listing.SubCategory.CategoryID == category.CategoryID ? true : false;

                        // convert to select list item and set selected attribte
                        SelectListItem categoryItem = new SelectListItem { Text = category.CategoryName, Value = category.CategoryID.ToString(), Selected = selected };
                        this.Categories.Add(categoryItem);
                    }

                    // get a list of subcategories belonging to the selected category
                    List<SubCategory> subcategories = db.SubCategories.Where(s => s.CategoryID == this.Listing.SubCategory.CategoryID).OrderBy(s => s.SubCategoryName).ToList();

                    foreach (SubCategory subcategory in subcategories)
                    {
                        // check to see if the subcategory is the current for the listing
                        bool selected = Listing.SubCategoryID == subcategory.SubCategoryID ? true : false;

                        // convert to select list item and set selected attribute
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