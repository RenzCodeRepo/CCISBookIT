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
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(string facultyId)
        {
            var user = _context.Users.Find(facultyId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
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

        public bool UserExists(string facultyId)
        {
            return _context.Users.Any(e => e.FacultyID == facultyId);
        }

        public bool UserLogin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(string facultyID)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.FacultyID == facultyID);
        }
    }
}
