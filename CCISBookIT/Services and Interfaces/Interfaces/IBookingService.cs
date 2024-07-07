﻿using CCISBookIT.Models;

namespace CCISBookIT.Services_and_Interfaces.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAll(); // Retrieves all Bookings.
        Task<IEnumerable<Booking>> FilterBooking(DateTime Date, Room RoomType, User FullName);
        Task<Booking> GetByBookingID(string BookingID);
        Task Add(Booking newBooking);
        Task Cancel(string BookingID);
        Task<bool> BookingExists(string BookingID);
        Task<bool> IsBookingOverlap(DateTime date, TimeOnly startTime, double duration, string roomNo);
        Task<List<Booking>> GetFilteredBookingsAsync(DateTime? filterDate, string filterStatus, string filterRoomNo, string filterUserId);
        byte[] GenerateCsvFile(List<Booking> bookings);
        Task UpdateExpiredBookings();
    }
}

