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
        void IBookingService.Add(Booking newBooking)
        {
            throw new NotImplementedException();
        }

        void IBookingService.Cancel(string BookingID)
        {
            throw new NotImplementedException();
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
            return await _context.Bookings.ToListAsync();
        }
    }
}
