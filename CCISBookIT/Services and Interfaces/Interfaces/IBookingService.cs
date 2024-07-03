using CCISBookIT.Models;

namespace CCISBookIT.Services_and_Interfaces.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAll(); // Retrieves all Bookings.
        Task<IEnumerable<Booking>> FilterBooking(DateTime Date, Room RoomType, User FullName);
        void Add(Booking newBooking);
        void Cancel(string BookingID);
        void GenerateReport(DateTime Date);

    }
}
