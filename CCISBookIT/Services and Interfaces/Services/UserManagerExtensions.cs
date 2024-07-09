using CCISBookIT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CCISBookIT.Services_and_Interfaces.Services
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByNameAsync(this UserManager<AppUser> um, string name)
        {
            return await um?.Users?.SingleOrDefaultAsync(x => x.UserName == name);
        }

        public static async Task<AppUser> FindByFacultyIDAsync(this UserManager<AppUser> um, string facultyID)
        {
            return await um?.Users?.SingleOrDefaultAsync(x => x.FacultyID == facultyID);
        }
    }
}
