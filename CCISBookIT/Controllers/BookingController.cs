using CCISBookIT.Data;
using Microsoft.AspNetCore.Mvc;

namespace CCISBookIT.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var booking = _context.Bookings.ToList();
            return View(booking);
        }
    }
}
