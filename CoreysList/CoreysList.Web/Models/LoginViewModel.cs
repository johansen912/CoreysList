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
    public class LoginViewModel
    {
         #region Constructors

        //constructor for the model
        public LoginViewModel()
        {
            CoreysListEntities Db = new CoreysListEntities();
        }

        #endregion

        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name ="Phone#:", Prompt = "###-###-####")]

        //[DisplayName("Phone#:")]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string LoginErrorMessage { get; set; }
        public string CreateAccountErrorMessage { get; set; }

        public string LoginEmail { get; set; }
        public string LoginPassword { get; set; }

        #endregion
    }
}