using CCISBookIT.Data;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CCISBookIT.Services_and_Interfaces.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        void IUserService.Add(User newUser)
        {
            throw new NotImplementedException();
        }

        void IUserService.Delete(string facultyId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.OrderBy(u => u.FacultyID).ToListAsync();
        }

        void IUserService.Update(User updatedUser)
        {
            throw new NotImplementedException();
        }

        bool IUserService.UserExists(string facultyId)
        {
            throw new NotImplementedException();
        }

        bool IUserService.UserLogin(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
