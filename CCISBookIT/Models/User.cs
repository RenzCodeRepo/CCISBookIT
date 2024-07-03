using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CCISBookIT.Data.Enum;

namespace CCISBookIT.Models
{
    public class User
    {
        [Key]
        [Display(Name = "Faculty ID")]
        public string FacultyID { get; set; }  // Faculty ID, serves as the primary key

        [Display(Name = "Full Name")]
        public string FullName { get; set; }  // Full name of the user

        [Display(Name = "Email")]
        public string Email { get; set; }  // Email address of the user

        [Display(Name = "Contact Number")]
        public string PhoneNumber { get; set; }  // Contact number of the user

        public string PasswordHash { get; set; }  // Hashed password of the user, consider using a secure method for storage

        [EnumDataType(typeof(UserRole))]
        [Display(Name = "Role")]
        public string Role { get; set; }  // Role of the user, validated against UserRole enum

        public ICollection<Booking> Bookings { get; set; }  // Navigation property to Booking entities

        public User()
        {
            Bookings = new List<Booking>();  // Initialize the navigation property collection in the constructor
        }
    }
}
