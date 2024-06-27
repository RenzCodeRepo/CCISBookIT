using System.ComponentModel.DataAnnotations;

namespace CCISBookIT.Models
{
    public class User_Booking
    {

        public string BookingId { get; set; }
        public Booking Booking { get; set; }

       
        public string FacultyId {  get; set; }  
    }
}
