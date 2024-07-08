using CCISBookIT.Data;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCISBookIT.Services_and_Interfaces.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        // Constructor injection of ApplicationDbContext
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Add a new user to the database
        public async Task Add(AppUser user)
        {
            if (await UserExists(user.FacultyID))
            {
                throw new ArgumentException($"User with FacultyID '{user.FacultyID}' already exists.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Delete a user from the database by FacultyID
        public async Task Delete(string facultyId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.FacultyID == facultyId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }   
        }


        // Retrieve all users sorted by FacultyID
        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await _context.Users.OrderBy(u => u.FacultyID).ToListAsync();
        }

        // Update user information in the database
        public async Task<AppUser> Update(string facultyId, AppUser updatedUser)
        {
            _context.Update(updatedUser);
            await _context.SaveChangesAsync();
            return updatedUser;
        }

        // Check if a user with a given FacultyID exists in the database
        public async Task<bool> UserExists(string facultyId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.FacultyID == facultyId);
            return user != null;
        }

        // Placeholder for user login functionality (not implemented)
        public bool UserLogin(string username, string password)
        {
            throw new NotImplementedException();
        }

        // Retrieve a user by FacultyID from the database
        public async Task<AppUser> GetById(string facultyId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.FacultyID == facultyId);
            if (user == null)
            {
                throw new ArgumentException($"User with FacultyID '{facultyId}' not found.");
            }
            return user;
        }

        public byte[] GenerateUsers()
        {
            var facultyUsers = _context.Users
           .Where(u => u.Role == "Faculty") // Adjust based on your user role structure
           .Select(u => new AppUser
           {
               FacultyID = u.FacultyID,
               FullName = u.FullName,
               Email = u.Email,
               PhoneNumber = u.PhoneNumber,
               Role = u.Role,
               // Map other properties as needed
           })
           .ToList();

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Faculty ID, Full Name, Email, Contact Number, Role");

            foreach (var user in facultyUsers)
            {
                csvBuilder.AppendLine($"{user.FacultyID},{user.FullName},{user.Email},{user.PhoneNumber},{user.Role}");
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }
    }
}
