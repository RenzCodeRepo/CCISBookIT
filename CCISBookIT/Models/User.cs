using CCISBookIT.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CCISBookIT.Models
{
    public class User
    {
        //structure of the table
        [Key]
        public string FacultyID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set;} 

     

        // Navigation property: each user can have many bookings
        public ICollection<Booking> Bookings { get; set; }
    }
}
