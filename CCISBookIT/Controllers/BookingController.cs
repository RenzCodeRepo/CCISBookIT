using CCISBookIT.Data;
using CCISBookIT.Data.Enum;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace CCISBookIT.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public BookingController(IBookingService bookingService, IRoomService roomService, UserManager<AppUser> userManager, AppDbContext context)
        {
            _context = context;
            _bookingService = bookingService;
            _roomService = roomService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(DateTime? filterDate, string filterStatus, string filterRoomNo)
        {
            await _bookingService.UpdateExpiredBookings();

            var bookings = await _bookingService.GetAll(); // Retrieve all bookings

            // Apply filters if provided
            if (filterDate.HasValue)
            {
                bookings = bookings.Where(b => b.Date.Date == filterDate.Value);
            }

            if (!string.IsNullOrEmpty(filterStatus))
            {
                bookings = bookings.Where(b => b.Status.ToString() == filterStatus);
            }

            if (!string.IsNullOrEmpty(filterRoomNo))
            {
                bookings = bookings.Where(b => b.RoomNo == filterRoomNo);
            }

            // Pass filters to view via ViewBag or model if needed
            ViewBag.FilterDate = filterDate;
            ViewBag.FilterStatus = filterStatus;
            ViewBag.FilterRoomNo = filterRoomNo;

            return View(bookings.ToList());
        }

        [HttpGet("Booking/Detail/{BookingId}")]
        public async Task<IActionResult> Detail(string BookingId)
        {
            var bookingDetail = await _bookingService.GetByBookingID(BookingId);
            if (bookingDetail == null)
            {
                return NotFound(); // Return 404 if user does not exist
            }

            return View(bookingDetail); // Pass user details to the "Edit" view
        }

        // Display cancellation confirmation page
        public async Task<IActionResult> CancelConfirmation(string BookingId)
        {
            if (string.IsNullOrEmpty(BookingId))
            {
                return BadRequest();
            }

            var booking = await _bookingService.GetByBookingID(BookingId);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // Handle the actual cancellation
        [HttpPost, ActionName("CancelConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelConfirmed(string BookingId)
        {
            if (string.IsNullOrEmpty(BookingId))
            {
                return BadRequest();
            }

            await _bookingService.Cancel(BookingId);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Booking/Create
        public async Task<IActionResult> Create()
        {
            var newBooking = new Booking
            {
                Status = "Active" // Set default role to "Faculty"
            };
            return View(newBooking); // Return create view with a new user instance;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking model)
        {
            // Ensure Status is set to Active
            model.Status = "Active";

            // Generate BookingId
            model.BookingId = $"{model.RoomNo}-{model.Date:yyyyMMdd}-{model.StartTime:HHmm}";

            // Calculate EndTime based on StartTime and Duration
            model.EndTime = model.StartTime.AddHours(model.Duration);

            // Check if the booking already exists
            bool existingBooking = await _bookingService.BookingExists(model.BookingId);

            if (existingBooking)
            {
                ModelState.AddModelError(string.Empty, "The same booking already exists. Please modify the booking details."); //applied in general not for each user
                return View(model);
            }

            // Check if the selected date is in the past
            if (model.Date.Date < DateTime.Today)
            {
                ModelState.AddModelError(nameof(model.Date), "Booking dates in the past are not allowed.");
                return View(model);
            }

            // Check if there's overlap with existing bookings
            bool isOverlap = await _bookingService.IsBookingOverlap(model.Date, model.StartTime, model.Duration, model.RoomNo);

            if (isOverlap)
            {
                ModelState.AddModelError(string.Empty, "Booking overlaps with an existing booking. Please choose a different Room.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                // Create the booking entity
                var booking = new Booking
                {
                    BookingId = model.BookingId,
                    Date = model.Date,
                    StartTime = model.StartTime,
                    Duration = model.Duration,
                    EndTime = model.EndTime,
                    Purpose = model.Purpose,
                    Status = model.Status,
                    RoomNo = model.RoomNo,
                    FacultyID = model.FacultyID 
                };

                // Add booking to the context and save changes
                await _bookingService.Add(booking);

                return RedirectToAction(nameof(Index));
            }

            // If ModelState is not valid, return the form with errors
            return View(model);
        }

        // Action to download CSV based on filters
        public async Task<IActionResult> DownloadBookingsCsv(DateTime? filterDate, string filterStatus, string filterRoomNo, string filterUserId)
        {
            var filteredBookings = await _bookingService.GetFilteredBookingsAsync(filterDate, filterStatus, filterRoomNo, filterUserId);

            // Generate CSV file content
            byte[] csvData = _bookingService.GenerateCsvFile(filteredBookings);

            // Generate file name with current date and filters
            string filters = GetFilterString(filterDate, filterStatus, filterRoomNo, filterUserId);
            string fileName = $"Bookings_{DateTime.Now.ToString("yyyyMMdd")}_{filters}.csv";

            // Return the CSV file as a FileResult with appropriate headers
            return File(csvData, "text/csv", fileName);
        }

        private string GetFilterString(DateTime? filterDate, string filterStatus, string filterRoomNo, string filterUserId)
        {
            // Example implementation of generating a filter string
            // Customize as per your filter requirements
            string filters = "";
            if (filterDate.HasValue)
                filters += $"Date-{filterDate.Value.ToString("yyyyMMdd")}_";
            if (!string.IsNullOrEmpty(filterStatus))
                filters += $"Status-{filterStatus}_";
            if (!string.IsNullOrEmpty(filterRoomNo))
                filters += $"RoomNo-{filterRoomNo}_";
            if (!string.IsNullOrEmpty(filterUserId))
                filters += $"UserId-{filterUserId}";

            // Remove trailing underscore
            if (filters.EndsWith("_"))
                filters = filters.Substring(0, filters.Length - 1);

            return filters;
        }

        [HttpGet]
        public async Task<IActionResult> ViewReservations()
        {
            var user = await _userManager.GetUserAsync(User);
            var userFacultyID = user.FacultyID; // Get the FacultyID from the current user

            var userBookings = await _context.Bookings
                .Where(b => b.FacultyID == userFacultyID)
                .ToListAsync();

            return View(userBookings);
        }
    }
}
