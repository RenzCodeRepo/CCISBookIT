using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CCISBookIT.Data.Enum;

namespace CCISBookIT.Models
{
    public class Room
    {
        [Key]
        [Display(Name = "Room Number")]
        public string RoomNo { get; set; }  // Room number, serves as the primary key

        [EnumDataType(typeof(RoomType))]
        [Display(Name = "Type of Room")]
        public string RoomType { get; set; }  // Type of room, validated against RoomType enum

        public ICollection<Booking> Bookings { get; set; }  // Navigation property to Booking entities

        public Room()
        {
            Bookings = new List<Booking>();  // Initialize the navigation property collection in the constructor
        }
    }
}
