using Microsoft.AspNetCore.Identity;

namespace CCISBookIT.Models
{
    public class AppUser : IdentityUser 
    {
        public new string FacultyID { get; set; }
        public new string FullName { get; set; }
        public ICollection<Booking> Bookings { get; set; }  // Navigation property to Booking entities

        public AppUser()
        {
            Bookings = new List<Booking>();  // Initialize the navigation property collection in the constructor
        }
    }
}
