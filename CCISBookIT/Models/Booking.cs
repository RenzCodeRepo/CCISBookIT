using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CCISBookIT.Data.Enum;

namespace CCISBookIT.Models
{
    public class Booking
    {
        [Key]
        [Display(Name = "Booking ID")]
        public string BookingId { get; set; } // Since BookingId is typically unique and non-nullable
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Start Time")]
        public TimeOnly StartTime { get; set; } // Consider using TimeSpan instead of TimeOnly for database compatibility
        [Display(Name = "Duration in Hours")]
        public double Duration { get; set; }
        [Display(Name = "End Time")]
        public TimeOnly EndTime { get; set; } // Consider using TimeSpan instead of TimeOnly for database compatibility
        [Display(Name = "Purpose")]
        public string Purpose { get; set; } // Purpose should generally be non-nullable unless it's truly optional
        [EnumDataType(typeof(Status))]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [ForeignKey("Room")]
        [Display(Name = "Room Number")]
        public string RoomNo { get; set; }
        public Room Room { get; set; }

        [ForeignKey("User")]
        [Display(Name = "Faculty ID")]
        public string FacultyId { get; set; }
        public User User { get; set; }
    }
}
