using CCISBookIT.Data;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        // Constructor injection of ApplicationDbContext
        public UserService(AppDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
        public async Task<List<AppUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
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

    }
}
