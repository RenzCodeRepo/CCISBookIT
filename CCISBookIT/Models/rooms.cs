using System.ComponentModel.DataAnnotations;

namespace CCISBookIT.Models
{
    public class rooms
    {
        [Key]
        public string roomNo { get; set; }
        public string roomType { get; set; }

    }
}
