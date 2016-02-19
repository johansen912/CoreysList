using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class DisplaySearchResultsViewModel
    {
        #region Constructors

        // constructor for the model
        public DisplaySearchResultsViewModel() 
        {
            CoreysListEntities db = new CoreysListEntities();

            // get all the states
            List<State> allStates = db.States.ToList();

            // instantiate lists of selectlistitems
            States = new List<SelectListItem>();
            Cities = new List<SelectListItem>();

            // convert all the states into selectlistitems
            foreach (State s in allStates)
            {
                SelectListItem castState = new SelectListItem();
                castState.Text = s.StateName;
                castState.Value = s.StateID.ToString();
                States.Add(castState);
            }
        }

        // Constructor that excepts search result
        public DisplaySearchResultsViewModel(string searchTerm)
        {
            CoreysListEntities db = new CoreysListEntities();

            // populate properties
            this.SearchTerm = searchTerm; 
            this.Listings = db.Listings.Where(l => l.Headline.Contains(searchTerm)
                || l.Description.Contains(searchTerm)).ToList();

            // instantiate lists of selectlistitems
            List<State> allStates = db.States.ToList();
            States = new List<SelectListItem>();
            Cities = new List<SelectListItem>();

            // assign values to slider filter
            PriceMaxFilter = 100000;
            PriceMinFilter = 0;

            // convert all the states into selectlistitems
            foreach (State s in allStates)
            {
                SelectListItem castState = new SelectListItem();
                castState.Text = s.StateName;
                castState.Value = s.StateID.ToString();
                States.Add(castState);
            }
        }

        #endregion

        #region Properties

        public string CityName { get; set; }

        public List<Listing> Listings { get; set; }

        public int TabId { get; set; }

        public List<SelectListItem> States { get; set; }

        public int SelectedStateIdFilter { get; set; }

        public List<SelectListItem> Cities { get; set; }

        public int SelectedCityIdFilter { get; set; }

        public int PriceMaxFilter { get; set; }

        public int PriceMinFilter { get; set; }

        public string SearchTerm { get; set; }

        #endregion
    }
}