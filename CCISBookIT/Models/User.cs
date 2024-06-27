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

        //relationship

        public List<Booking> Booking { get; set; }

    }
}
