using CCISBookIT.Models; // Assuming Room model is in CCISBookIT.Models namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCISBookIT.Services_and_Interfaces.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAll(); // Retrieves all rooms.
        Task<IEnumerable<Room>> GetRoomsByType(string roomType); // Retrieves rooms by room type.
        Room GetbyRoomNo(string roomNo); // Retrieves a room by room number.
        void Add(Room newRoom); // Adds a new room.
        bool RoomExists(string roomNo); // Checks if a room exists by room number.
        void Update(Room updatedRoom); // Updates an existing room.
        void Delete(string roomNo); // Deletes a room by room number.
    }
}
