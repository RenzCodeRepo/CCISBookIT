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
    public class UserController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        public async Task<IActionResult> Index()
        {
            var users = (await _userService.GetAll()).OrderBy(u => u.FacultyID).ToList(); // Retrieve and sort users by FacultyID

            return View(users); // Pass sorted users to the "Index" view
        }

        [HttpGet("User/Detail/{facultyId}")]
        public async Task<IActionResult> Detail(string facultyID)
        {
            var user = await _userService.GetById(facultyID);

            if (user == null)
            {
                return NotFound(); // Return 404 if user does not exist
            }

            return View(user); // Pass user details to the "Detail" view
        }

        [HttpGet("User/Edit/{facultyId}")]
        public async Task<IActionResult> Edit(string facultyId)
        {
            var userDetail = await _userService.GetById(facultyId);
            if (userDetail == null) return View("Not Found");

            return View(userDetail); // Pass user details to the "Edit" view
        }

        [HttpPost("User/Edit/{facultyId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string FacultyId,[Bind("FacultyID, FullName, Email, PhoneNumber, PasswordHash, Role")] User updatedUser)
        {
           if (!ModelState.IsValid)
           {
                return View(updatedUser);
           }
            await _userService.Update(FacultyId, updatedUser);
            return RedirectToAction(nameof(Index));
        }

        //Get: User/Create

        public IActionResult Create()
        {
            var newUser = new User
            {
                Role = "Faculty" // Set default role to "Faculty"
            };
            return View(newUser);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FacultyID, FullName, Email, PhoneNumber, PasswordHash, Role")] User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (await _userService.UserExists(user.FacultyID))
            {
                ModelState.AddModelError(string.Empty, $"User with FacultyID '{user.FacultyID}' already exists.");
                return View(user);
            }

            if (string.IsNullOrEmpty(user.Role))
            {
                user.Role = "Faculty";
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await _userService.Add(user);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(string FacultyID)
        {
            var user = await _userService.GetById(FacultyID);
            if (user == null) return View("Not Found");

            return View(user);
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(string FacultyID)
        {
            _userService.Delete(FacultyID);
            return RedirectToAction(nameof(Index));
        }
    }
}
