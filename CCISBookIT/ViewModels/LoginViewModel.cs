using System.ComponentModel.DataAnnotations;

namespace CCISBookIT.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Faculty ID")]
        [Required(ErrorMessage = "Faculty ID is required")]
        public string FacultyID { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
