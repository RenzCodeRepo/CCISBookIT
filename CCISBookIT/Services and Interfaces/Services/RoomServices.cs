using CCISBookIT.Data;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CCISBookIT.Services_and_Interfaces.Services
{
    public class RoomServices : IRoomService
    {
        private readonly ApplicationDbContext _context;
        public RoomServices(ApplicationDbContext context)
        {
            _context = context;
        }
        void IRoomService.Add(Room newRoom)
        {
            throw new NotImplementedException();
        }

        void IRoomService.Delete(string roomNo)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _context.Rooms.ToListAsync();
        }

        Room IRoomService.GetbyRoomNo(string roomNo)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Room>> IRoomService.GetRoomsByType(string roomType)
        {
            throw new NotImplementedException();
        }

        bool IRoomService.RoomExists(string roomNo)
        {
            throw new NotImplementedException();
        }

        void IRoomService.Update(Room updatedRoom)
        {
            throw new NotImplementedException();
        }
    }
}
