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
      
        [ForeignKey("User")]
        public string FacultyId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public int Duration { get; set; }
        public TimeOnly EndTime { get; set; }
        [ForeignKey("Rooms")]
        public string RoomNo { get; set; }
        public string Purpose { get; set; }
        public Status Status { get; set; }

        //relationships

        public List<User> User { get; set; }

        public List<Room> Room { get; set; }
  

    }
}
