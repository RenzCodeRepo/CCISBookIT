using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CCISBookIT.Data.Enum;
using Microsoft.AspNetCore.Identity;

namespace CCISBookIT.Models
{
    public class AppUser : IdentityUser 
    {
        
        [RegularExpression(@"^CCISF\d{3}$", ErrorMessage = "Faculty ID must be in the format CCISF followed by three digits.")]
        [Display(Name = "Faculty ID")]
        public string FacultyID { get; set; }  // Faculty ID, serves as the primary key

        [Required(ErrorMessage = "Full Name is required.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }  // Full name of the user

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }  // Email address of the user

        [Required(ErrorMessage = "Contact Number is required.")]
        [Display(Name = "Contact Number")]
        public string PhoneNumber { get; set; }  // Contact number of the user

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }  // Hashed password of the user, consider using a secure method for storage

        [EnumDataType(typeof(UserRole))]
        [Display(Name = "Role")]
        public string Role { get; set; }  // Role of the user, validated against UserRole enum

        public ICollection<Booking> Bookings { get; set; }  // Navigation property to Booking entities

        public AppUser()
        {
            Bookings = new List<Booking>();  // Initialize the navigation property collection in the constructor
        }
    }
}
