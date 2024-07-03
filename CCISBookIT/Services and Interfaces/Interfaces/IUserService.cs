using CCISBookIT.Models; // Assuming User model is in CCISBookIT.Models namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCISBookIT.Services_and_Interfaces.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll(); // Retrieves all users.
        void Add(User newUser); // Adds a new user.
        bool UserExists(string facultyId); // Checks if a user exists by their Faculty ID.
        bool UserLogin(string username, string password);
        void Update(User updatedUser); // Updates an existing user.
        void Delete(string facultyId); // Deletes a user by their Faculty ID.
    }
}
