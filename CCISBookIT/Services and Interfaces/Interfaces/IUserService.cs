using CCISBookIT.Models; // Assuming User model is in CCISBookIT.Models namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCISBookIT.Services_and_Interfaces.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetAll(); // Retrieves all users asynchronously.

        Task Add(AppUser user); // Adds a new user synchronously.

        Task<bool> UserExists(string facultyId); // Checks if a user exists by their Faculty ID.

        bool UserLogin(string username, string password); // Placeholder for user login functionality.

        Task<AppUser> Update(string facultyId, AppUser updatedUser); // Updates an existing user asynchronously.

        Task Delete(string facultyId); // Deletes a user by their Faculty ID asynchronously.

        Task<AppUser> GetById(string facultyId); // Retrieves a user by their Faculty ID asynchronously.
        byte[] GenerateUsers();
    }
}
