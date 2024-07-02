using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CCISBookIT.Data.Enum;

namespace CCISBookIT.Models
{
    public class User
    {
        [Key]
        [Display(Name = "Faculty ID")]
        public string FacultyID { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Contact Number")]
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        [EnumDataType(typeof(UserRole))]
        [Display(Name = "Role")]
        public string Role { get; set; }

        public ICollection<Booking> Bookings { get; set; } // Navigation property

        public User()
        {
            Bookings = new List<Booking>(); // Initialize navigation property collection in constructor
        }
    }
}
