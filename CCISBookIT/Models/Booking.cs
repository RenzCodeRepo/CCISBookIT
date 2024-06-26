using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCISBookIT.Models
{
    public class Booking
    {
        [Key]
        public string bookingId { get; set; }
        [ForeignKey("User")]
        public string facultyId { get; set; }
        public DateTime date { get; set; }
        public TimeOnly startTime { get; set; }
        public int duration { get; set; }
        public TimeOnly endTime { get; set; }
        [ForeignKey("Rooms")]
        public string roomNo { get; set; }
        public string purpose { get; set; }
        public string status { get; set; }

    }
}
