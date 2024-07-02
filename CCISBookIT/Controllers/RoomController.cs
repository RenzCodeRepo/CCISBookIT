using CCISBookIT.Data;
using Microsoft.AspNetCore.Mvc;

namespace CCISBookIT.Controllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var rooms = _context.Rooms.ToList();
            return View(rooms);
        }
    }
}
