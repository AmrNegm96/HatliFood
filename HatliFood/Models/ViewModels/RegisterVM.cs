
using System.ComponentModel.DataAnnotations;

namespace HatliFood.Models.ViewModels
{
    public enum Gender
    {
        Male, Female
    }
    public class RegisterVM
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is required")]
        public string FisrtName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Gender gender { get; set; }


        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }


    }

   
}
