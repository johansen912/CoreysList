using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class LocatorViewModel
    {
         #region Constructors

        //constructor for the model
        public LocatorViewModel()
        {
            CoreysListEntities Db = new CoreysListEntities();
            this.States1 = Db.States.Where(c => c.StateID <= 120).OrderBy(c => c.StateName).ToList();
            this.States2 = Db.States.Where(c => c.StateID > 120 && c.StateID <= 133).OrderBy(c => c.StateName).ToList();
            this.States3 = Db.States.Where(c => c.StateID > 133 && c.StateID <= 146).OrderBy(c => c.StateName).ToList();
            this.States4 = Db.States.Where(c => c.StateID > 146).OrderBy(c => c.StateName).ToList();
            
        }

        #endregion

        #region Properties

        public List<State> States1 { get; set; }
        public List<State> States2 { get; set; }
        public List<State> States3 { get; set; }
        public List<State> States4 { get; set; }

        #endregion
    }
}