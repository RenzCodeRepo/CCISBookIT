using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CCISBookIT.Data.Enum;

namespace CCISBookIT.Models
{
    public class Booking
    {
        [Key]
        public string BookingId { get; set; } // Since BookingId is typically unique and non-nullable

        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; } // Consider using TimeSpan instead of TimeOnly for database compatibility
        public double Duration { get; set; }
        public TimeOnly EndTime { get; set; } // Consider using TimeSpan instead of TimeOnly for database compatibility
        public string Purpose { get; set; } // Purpose should generally be non-nullable unless it's truly optional
        [EnumDataType(typeof(Status))]
        public string Status { get; set; }

        [ForeignKey("Room")]
        public string RoomNo { get; set; }
        public Room Room { get; set; }

        [ForeignKey("User")]
        public string FacultyId { get; set; }
        public User User { get; set; }
    }
}
