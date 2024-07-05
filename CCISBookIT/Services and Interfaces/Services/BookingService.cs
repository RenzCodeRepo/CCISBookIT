using CCISBookIT.Data;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CCISBookIT.Services_and_Interfaces.Services
{
    public class BookingService : IBookingService
    {

        private readonly ApplicationDbContext _context;
        public BookingService(ApplicationDbContext context)
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

        Task<IEnumerable<Booking>> IBookingService.FilterBooking(DateTime Date, Room RoomType, User FullName)
        {
            throw new NotImplementedException();
        }

        void IBookingService.GenerateReport(DateTime Date)
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
    }
}
