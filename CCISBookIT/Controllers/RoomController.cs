using CCISBookIT.Data;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CCISBookIT.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService; // Constructor injection
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAll(); // Retrieve all rooms asynchronously

            return View(rooms); // Pass rooms to the "Index" view
        }
    }
}
