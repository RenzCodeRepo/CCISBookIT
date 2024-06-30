using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CCISBookIT.Data.Enum;

namespace CCISBookIT.Models
{
    public class User
    {
        [Key]
        public string FacultyID { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        [EnumDataType(typeof(UserRole))]
        public string Role { get; set; }

        public ICollection<Booking> Bookings { get; set; } // Navigation property

        public User()
        {
            Bookings = new List<Booking>(); // Initialize navigation property collection in constructor
        }
    }
}
