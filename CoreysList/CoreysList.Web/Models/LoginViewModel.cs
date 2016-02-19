using System.ComponentModel.DataAnnotations;
using CoreysList.Entity;

namespace CoreysList.Web.Models
{
    public class LoginViewModel
    {
         #region Constructors

        // constructor for the model
        public LoginViewModel()
        {
        }

        #endregion

        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone#:", Prompt = "###-###-####")]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string LoginErrorMessage { get; set; }

        public string CreateAccountErrorMessage { get; set; }

        public string LoginEmail { get; set; }

        public string LoginPassword { get; set; }

        #endregion
    }
}