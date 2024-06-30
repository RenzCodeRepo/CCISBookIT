using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CCISBookIT.Data.Enum;

namespace CCISBookIT.Models
{
    public class Room
    {
        [Key]
        public string RoomNo { get; set; }
        [EnumDataType(typeof(RoomType))]
        public string RoomType { get; set; }

        public ICollection<Booking> Bookings { get; set; } // Navigation property

        public Room()
        {
            Bookings = new List<Booking>(); // Initialize navigation property collection in constructor
        }
    }
}
