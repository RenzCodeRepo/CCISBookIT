using CCISBookIT.Data;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CCISBookIT.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService; // Declare IUserService interface

        // Constructor injection of IUserService
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User/Index
        public async Task<IActionResult> Index()
        {
            var users = (await _userService.GetAll()).OrderBy(u => u.FacultyID).ToList(); // Retrieve and sort users by FacultyID

            return View(users); // Pass sorted users to the "Index" view
        }

        // GET: User/Detail/{facultyId}
        [HttpGet("User/Detail/{facultyId}")]
        public async Task<IActionResult> Detail(string facultyId)
        {
            var user = await _userService.GetById(facultyId);

            if (user == null)
            {
                return NotFound(); // Return 404 if user does not exist
            }

            return View(user); // Pass user details to the "Detail" view
        }

        // GET: User/Edit/{facultyId}
        [HttpGet("User/Edit/{facultyId}")]
        public async Task<IActionResult> Edit(string facultyId)
        {
            var userDetail = await _userService.GetById(facultyId);
            if (userDetail == null)
            {
                return NotFound(); // Return 404 if user does not exist
            }

            return View(userDetail); // Pass user details to the "Edit" view
        }

        // POST: User/Edit/{facultyId}
        [HttpPost("User/Edit/{facultyId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string facultyId, [Bind("FacultyID, FullName, Email, PhoneNumber, PasswordHash, Role")] User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedUser); // Return the edit view with validation errors
            }

            await _userService.Update(facultyId, updatedUser); // Update user details
            return RedirectToAction(nameof(Index)); // Redirect to Index action after successful edit
        }

        // GET: User/Create
        public IActionResult Create()
        {
            var newUser = new User
            {
                Role = "Faculty" // Set default role to "Faculty"
            };
            return View(newUser); // Return create view with a new user instance
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacultyID, FullName, Email, PhoneNumber, PasswordHash, Role")] User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user); // Return the create view with validation errors
            }

            if (await _userService.UserExists(user.FacultyID))
            {
                ModelState.AddModelError(string.Empty, $"User with FacultyID '{user.FacultyID}' already exists.");
                return View(user); // Return the create view with error message if user already exists
            }

            if (string.IsNullOrEmpty(user.Role))
            {
                user.Role = "Faculty"; // Set default role if not provided
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash); // Hash the user's password
            await _userService.Add(user); // Add new user asynchronously
            return RedirectToAction(nameof(Index)); // Redirect to Index action after successful creation
        }

        // GET: User/Delete/{facultyId}
        public async Task<IActionResult> Delete(string facultyId)
        {
            var user = await _userService.GetById(facultyId);
            if (user == null)
            {
                return NotFound(); // Return 404 if user does not exist
            }

            return View(user); // Pass user details to the "Delete" view
        }

        // POST: User/DeleteConfirmed/{facultyId}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string facultyId)
        {
            _userService.Delete(facultyId); // Delete user by FacultyID
            return RedirectToAction(nameof(Index)); // Redirect to Index action after successful deletion
        }
    }
}
