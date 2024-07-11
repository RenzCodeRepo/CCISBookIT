using System;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;

namespace CCISBookIT.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Faculty ID")]
        [Required(ErrorMessage = "Faculty ID is required.")]
        [RegularExpression(@"^(CCISF|CCISA)\d{3}$", ErrorMessage = "Faculty ID is invalid.")]
        public string FacultyID { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "PhoneNumber is required.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
