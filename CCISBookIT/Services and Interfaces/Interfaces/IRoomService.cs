using CCISBookIT.Models; // Assuming Room model is in CCISBookIT.Models namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCISBookIT.Services_and_Interfaces.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAll(); // Retrieves all rooms.
        Task<IEnumerable<Room>> GetRoomsByType(string roomType); // Retrieves rooms by room type.
        Task<Room> GetbyRoomNo(string roomNo); // Retrieves a room by room number.
        bool RoomExists(string roomNo); // Checks if a room exists by room number.
        Task<Room> Update(string roomNo, Room updatedRoom); // Updates an existing room
    }
}
