using CCISBookIT.Models;

namespace CCISBookIT.Services_and_Interfaces.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAll(); // Retrieves all Bookings.
        Task<IEnumerable<Booking>> FilterBooking(DateTime Date, Room RoomType, User FullName);
        Task<Booking> GetByBookingID(string BookingID);
        Task Add(Booking newBooking);
        Task Cancel(string BookingID);
        void GenerateReport(DateTime Date);
        Task<bool> BookingExists(string BookingID);

    }
}
