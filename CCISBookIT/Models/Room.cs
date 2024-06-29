using System.ComponentModel.DataAnnotations;
using CCISBookIT.Data.Enum;

namespace CCISBookIT.Models
{
    public class Room
    {
        //structure of the table
        [Key]
        public string RoomNo { get; set; }
        public RoomType RoomType { get; set; }
        // Navigation property: each room can have many bookings
        public ICollection<Booking> Bookings { get; set; }

    }
}
