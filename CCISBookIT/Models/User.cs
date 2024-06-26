using System.ComponentModel.DataAnnotations;

namespace CCISBookIT.Models
{
    public class User
    {
        [Key]
        public string facultyID { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }

    }
}
