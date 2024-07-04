using CCISBookIT.Data;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCISBookIT.Services_and_Interfaces.Services
{
    public class UserService(ApplicationDbContext context) : IUserService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task Add(User user)
        {
            if (await UserExists(user.FacultyID))
            {
                throw new ArgumentException($"User with FacultyID '{user.FacultyID}' already exists.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task Delete(string facultyId)
        {
            var user = _context.Users.FirstOrDefault(u => u.FacultyID == facultyId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.OrderBy(u => u.FacultyID).ToListAsync();
        }

        public async Task<User> Update(string FacultyID, User updatedUser)
        {
            _context.Update(updatedUser);
            await _context.SaveChangesAsync();
            return updatedUser;
        }

        public async Task<bool> UserExists(string facultyId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.FacultyID == facultyId);
            return user != null;
        }

        public bool UserLogin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(string facultyId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.FacultyID == facultyId);
            if (user == null)
            {
                throw new ArgumentException($"User with FacultyID '{facultyId}' not found.");
                // or alternatively return a default user
                // return new User { FacultyID = facultyId, FullName = "Unknown", Email = "Unknown" };
            }
            return user;
        }
    }
}
