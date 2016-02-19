using System;
using System.Collections.Generic;
using System.Linq;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class StateSelectorViewModel
    {
        #region Constructors

        // constructor for the model
        public StateSelectorViewModel(int stateId)
        {
            // Get the selected state and the cites that belong to it
            CoreysListEntities db = new CoreysListEntities();
            this.Cities = db.Cities.Where(c => c.StateID == stateId).ToList();
            this.StateName = db.States.FirstOrDefault(s => s.StateID == stateId).StateName;
        }

        #endregion

        #region Properties

        public List<City> Cities { get; set; }

        public string StateName { get; set; }

        #endregion
    }
}