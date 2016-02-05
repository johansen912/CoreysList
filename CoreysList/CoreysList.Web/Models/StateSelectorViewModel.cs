using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class StateSelectorViewModel
    {
        #region Constructors

        //constructor for the model
        public StateSelectorViewModel( int stateId)
        {
            CoreysListEntities Db = new CoreysListEntities();
            this.Cities = Db.Cities.Where(c => c.StateID == stateId).ToList();
            this.StateName = Db.States.FirstOrDefault(s => s.StateID == stateId).StateName;
        }

        #endregion

        #region Properties
        public List<City> Cities { get; set; }
        public String StateName { get; set; }

        #endregion
    }
}