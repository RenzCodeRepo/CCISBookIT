using System.ComponentModel.DataAnnotations;

namespace CCISBookIT.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Faculty ID is required.")]
        [RegularExpression(@"^CCISF\d{3}$", ErrorMessage = "Faculty ID must be in the format CCISF followed by three digits.")]
        [Display(Name = "Faculty ID")]
        public string FacultyID { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
