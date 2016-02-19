using System.Collections.Generic;
using System.Linq;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class LocatorViewModel
    {
         #region Constructors

        // constructor for the model
        public LocatorViewModel()
        {
            // populate properties for page layout
            CoreysListEntities db = new CoreysListEntities();
            this.States1 = db.States.Where(c => c.StateID <= 120).OrderBy(c => c.StateName).ToList();
            this.States2 = db.States.Where(c => c.StateID > 120 && c.StateID <= 133).OrderBy(c => c.StateName).ToList();
            this.States3 = db.States.Where(c => c.StateID > 133 && c.StateID <= 146).OrderBy(c => c.StateName).ToList();
            this.States4 = db.States.Where(c => c.StateID > 146).OrderBy(c => c.StateName).ToList();
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