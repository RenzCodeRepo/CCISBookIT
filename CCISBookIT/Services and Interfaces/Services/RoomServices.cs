﻿using CCISBookIT.Data;
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

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetbyRoomNo(string roomNo)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNo == roomNo);
            if (room == null)
            {
                throw new ArgumentException($"Room with '{roomNo}' not found.");
            }
            return room;
        }

        Task<IEnumerable<Room>> IRoomService.GetRoomsByType(string roomType)
        {
            throw new NotImplementedException();
        }

        bool IRoomService.RoomExists(string roomNo)
        {
            throw new NotImplementedException();
        }

        public async Task<Room> Update(string roomNo, Room updatedRoom)
        {
            _context.Update(updatedRoom);
            await _context.SaveChangesAsync();
            return updatedRoom;
        }
    }
}
