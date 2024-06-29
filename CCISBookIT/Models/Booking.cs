using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CCISBookIT.Data.Enum;

namespace CCISBookIT.Models
{
    public class Booking
    {
        //structure of the table
        [Key]
        public string BookingId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public Double Duration { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Purpose { get; set; }
        public Status Status { get; set; }

        [ForeignKey("Room")]
        public string RoomNo { get; set; }
        public Room Room { get; set; } // Navigation property

        [ForeignKey("User")]
        public string FacultyId { get; set; }
        public User User { get; set; } // Navigation property
    }
}
