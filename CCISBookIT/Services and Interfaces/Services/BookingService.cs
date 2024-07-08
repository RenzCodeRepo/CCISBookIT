using CCISBookIT.Data;
using CCISBookIT.Data.Enum;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CCISBookIT.Services_and_Interfaces.Services
{
    public class BookingService : IBookingService
    {

        private readonly AppDbContext _context;
        public BookingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Booking newBooking)
        {
            if (newBooking == null)
            {
                throw new ArgumentNullException(nameof(newBooking));
            }

            _context.Bookings.Add(newBooking);
            await _context.SaveChangesAsync();
        }

        public async Task Cancel(string BookingID)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == BookingID);
            if (booking == null)
            {
                throw new ArgumentException($"Booking with Booking ID '{BookingID}' not found.");
            }

            booking.Status = "Cancelled";
            await _context.SaveChangesAsync();
        }

        Task<IEnumerable<Booking>> IBookingService.FilterBooking(DateTime Date, Room RoomType, AppUser FullName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _context.Bookings.OrderByDescending(b => b.Date).ToListAsync();
        }

        public async Task<Booking> GetByBookingID(string BookingID)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == BookingID);
            if (booking == null) { throw new ArgumentException($"Booking with Booking ID '{BookingID}' not found."); }
            return booking;
        }

        public async Task<bool> BookingExists(string BookingID)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == BookingID);
            return booking != null;
        }
        public async Task<bool> IsBookingOverlap(DateTime date, TimeOnly startTime, double duration, string roomNo)
        {
            TimeOnly endTime = startTime.AddHours(duration);
            var overlappingBooking = await _context.Bookings
                .Where(b => b.Date == date && b.RoomNo == roomNo && b.Status != "Cancelled" &&
                            ((startTime >= b.StartTime && startTime < b.StartTime.AddHours(b.Duration)) ||
                             (endTime > b.StartTime && endTime <= b.StartTime.AddHours(b.Duration)) ||
                             (startTime <= b.StartTime && endTime >= b.StartTime.AddHours(b.Duration))))
                .FirstOrDefaultAsync();

            return overlappingBooking != null;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync(); // Example, adjust to retrieve bookings from your database
        }

        public async Task<List<Booking>> GetFilteredBookingsAsync(DateTime? filterDate, string filterStatus, string filterRoomNo, string filterUserId)
        {
            var bookings = _context.Bookings.AsQueryable(); // Start with all bookings

            // Apply filters
            if (filterDate.HasValue)
            {
                bookings = bookings.Where(b => b.Date.Date == filterDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(filterStatus))
            {
                bookings = bookings.Where(b => b.Status.ToString() == filterStatus);
            }

            if (!string.IsNullOrEmpty(filterRoomNo))
            {
                bookings = bookings.Where(b => b.RoomNo == filterRoomNo);
            }

            if (!string.IsNullOrEmpty(filterUserId))
            {
                bookings = bookings.Where(b => b.FacultyId == filterUserId);
            }

            return await bookings.ToListAsync();
        }


        public byte[] GenerateCsvFile(List<Booking> bookings)
        {
            StringBuilder sb = new StringBuilder();

            // Header
            sb.AppendLine("BookingId,Date,StartTime,Duration,EndTime,Purpose,Status,RoomNo,FacultyId");

            // Data rows
            foreach (var booking in bookings)
            {
                sb.AppendLine($"{booking.BookingId},{booking.Date},{booking.StartTime},{booking.Duration},{booking.EndTime},{booking.Purpose},{booking.Status},{booking.RoomNo},{booking.FacultyId}");
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public async Task UpdateExpiredBookings()
        {
            var bookings = await _context.Bookings
                .Where(b => b.Status == Status.Active.ToString())
                .ToListAsync();

            foreach (var booking in bookings)
            {
                if (booking.Date < DateTime.Now.Date ||
                    (booking.Date == DateTime.Now.Date && booking.EndTime <= TimeOnly.FromDateTime(DateTime.Now)))
                {
                    booking.Status = Status.Expired.ToString();
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
