using CCISBookIT.Data;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using CCISBookIT.Services_and_Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CCISBookIT.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly AppDbContext _context;

        public RoomController(IRoomService roomService, AppDbContext context)
        {
            _roomService = roomService; // Constructor injection
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAll(); // Retrieve all rooms asynchronously

            return View(rooms); // Pass rooms to the "Index" view
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Room/Detail/{RoomNo}")]
        public async Task<IActionResult> Detail(string RoomNo)
        {
            var room = await _roomService.GetbyRoomNo(RoomNo);

            if (room == null)
            {
                return NotFound(); // Return 404 if user does not exist
            }

            return View(room); // Pass user details to the "Detail" view
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Room/Edit/{RoomNo}")]
        public async Task<IActionResult> Edit(string RoomNo)
        {
            var roomNo = await _roomService.GetbyRoomNo(RoomNo);
            if (roomNo == null)
            {
                return NotFound();
            }
            return View(roomNo);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Room/Edit/{RoomNo}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string RoomNo,[Bind("RoomNo, RoomType")] Room updatedRoom)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedRoom);
            }

            await _roomService.Update(RoomNo, updatedRoom);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ExportCsv()
        {
            var rooms = await _context.Rooms.ToListAsync();
            var csv = _roomService.GenerateCsvFile(rooms);
            var fileName = $"RoomsReport_{DateTime.Now:yyyyMMdd}.csv";
            return File(csv, "text/csv", fileName);
        }

    }
}
