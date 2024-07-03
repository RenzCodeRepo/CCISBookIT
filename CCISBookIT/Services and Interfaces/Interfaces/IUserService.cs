using CCISBookIT.Models; // Assuming User model is in CCISBookIT.Models namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCISBookIT.Services_and_Interfaces.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll(); // Retrieves all users asynchronously.

        void Add(User user); // Adds a new user synchronously.

        bool UserExists(string facultyId); // Checks if a user exists by their Faculty ID.

        bool UserLogin(string username, string password); // Placeholder for user login functionality.

        Task<User> Update(string FacultyID, User updatedUser); // Updates an existing user asynchronously.

        void Delete(string facultyId); // Deletes a user by their Faculty ID synchronously.

        Task<User> GetById(string facultyId); // Retrieves a user by their Faculty ID asynchronously.
    }
}
