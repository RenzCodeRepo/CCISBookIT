using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CCISBookIT.Data.Enum;  // Assuming Status enum is defined here

namespace CCISBookIT.Models
{
    public class Booking
    {
        [Key]
        [Display(Name = "Booking ID")]
        public string BookingId { get; set; }  // Primary key for Booking

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }  // Date of the booking

        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "Start Time is required")]
        public TimeOnly StartTime { get; set; }  // Consider using TimeSpan for database compatibility

        [Display(Name = "Duration in Hours")]
        [Required(ErrorMessage = "Duration is required")]
        public double Duration { get; set; }  // Duration of the booking in hours

        [Display(Name = "End Time")]
        public TimeOnly EndTime { get; set; }  // Consider using TimeSpan for database compatibility

        [Display(Name = "Purpose")]
        [Required(ErrorMessage = "Purpose is required")]
        public string Purpose { get; set; }  // Purpose of the booking

        [EnumDataType(typeof(Status))]
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }  // Status of the booking, should match Status enum values

        [ForeignKey("Room")]
        [Display(Name = "Room Number")]
        [Required(ErrorMessage = "Room Number is required")]
        public string RoomNo { get; set; }  // Foreign key to Room entity

        public Room Room { get; set; }  // Navigation property to Room

        [ForeignKey("User")]
        [Display(Name = "Faculty ID")]
        [Required(ErrorMessage = "Faculty ID is required")]
        public string FacultyId { get; set; }  // Foreign key to User entity

        public User User { get; set; }  // Navigation property to User
        public Booking()
        {
            Date = DateTime.Today; // Initialize with today's date or any other default date
        }
    }
}
