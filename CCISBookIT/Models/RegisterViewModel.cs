using System.ComponentModel.DataAnnotations;

namespace CCISBookIT.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Faculty ID is required.")]
        [RegularExpression(@"^CCISF\d{3}$", ErrorMessage = "Faculty ID must be in the format CCISF followed by three digits.")]
        [Display(Name = "Faculty ID")]
        public string FacultyID { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [Display(Name = "Contact Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
