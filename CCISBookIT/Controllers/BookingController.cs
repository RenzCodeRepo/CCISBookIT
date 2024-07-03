using CCISBookIT.Data;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CCISBookIT.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService; // Constructor injection
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAll(); // Retrieve all bookings

            return View(bookings); // Pass bookings to the "Index" view
        }
    }
}
