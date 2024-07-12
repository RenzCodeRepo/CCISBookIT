using CCISBookIT.Data;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using CCISBookIT.Services_and_Interfaces.Services;
using CCISBookIT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCISBookIT.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService; // Declare IUserService interface
        private readonly UserManager<AppUser> _userManager;


        // Constructor injection of IUserService
        public UserController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [Authorize(Roles ="Admin")]
        // GET: User/Index
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            var usersWithoutAdmins = new List<AppUser>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Admin"))
                {
                    usersWithoutAdmins.Add(user);
                }
            }

            var sortedUsers = usersWithoutAdmins.OrderBy(u => u.FacultyID).ToList(); // Sort users by FacultyID

            return View(sortedUsers); // Pass sorted users to the "Index" view
        }

        [Authorize]
        // GET: User/Detail/{facultyId}
        [HttpGet("User/Detail/{facultyId}")]
        public async Task<IActionResult> Detail(string facultyId)
        {
            var user = await _userManager.FindByFacultyIDAsync(facultyId);

            if (user == null)
            {
                return NotFound(); // Return 404 if user does not exist
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var model = new UserDetailViewModel
            {
                User = user,
                IsAdmin = isAdmin
            };

            return View(model); // Pass user details to the "Detail" view
        }

        [Authorize]
        // GET: User/Edit/{facultyId}
        [HttpGet("User/Edit/{facultyId}")]
        public async Task<IActionResult> Edit(string facultyId)
        {
            var userDetail = await _userManager.FindByFacultyIDAsync(facultyId);
            if (userDetail == null)
            {
                return NotFound(); // Return 404 if user does not exist
            }
            var editUserVM = new EditUserViewModel
            {
                FacultyID = userDetail.FacultyID,
                FullName = userDetail.FullName,
                Email = userDetail.Email,
                PhoneNumber = userDetail.PhoneNumber
            };
            return View(editUserVM); // Pass user details to the "Edit" view
        }

        [Authorize]
        // POST: User/Edit/{facultyId}
        [HttpPost("User/Edit/{facultyId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string facultyId, EditUserViewModel editUserVM)
        {
            if (facultyId != editUserVM.FacultyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByFacultyIDAsync(facultyId);
                if (user == null)
                {
                    return NotFound();
                }

                user.FacultyID = editUserVM.FacultyID;
                user.FullName = editUserVM.FullName;
                user.Email = editUserVM.Email;
                user.PhoneNumber = editUserVM.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Detail", "User", new { id = editUserVM.FacultyID });
                }
            }

            return View(editUserVM);
        }

        [Authorize]
        // GET: User/Delete/{facultyId}
        public async Task<IActionResult> Delete(string facultyId)
        {
            var user = await _userManager.FindByFacultyIDAsync(facultyId);
            if (user == null)
            {
                return NotFound(); // Return 404 if user does not exist
            }

            return View(user); // Pass user details to the "Delete" view
        }

        [Authorize] 
        // POST: User/DeleteConfirmed/{facultyId}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string facultyId)
        {
            if (string.IsNullOrEmpty(facultyId))
            {
                return NotFound(); // Return 404 if facultyId is null or empty
            }
            await _userService.Delete(facultyId);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        // Action to download users as CSV with current date in file name
        public async Task<IActionResult> DownloadFacultyUsersCsv()
        {
            var users = await _userService.GetAllUsersAsync();

            // Prepare CSV content
            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("FacultyID,FullName,Email,PhoneNumber");

            foreach (var user in users)
            {
                csvContent.AppendLine($"{user.FacultyID},{user.FullName},{user.Email},{user.PhoneNumber}");
            }

            // Prepare file name with current date
            string fileName = $"users_{DateTime.Now:yyyyMMdd}.csv";

            // Prepare and return the CSV file as a downloadable file
            byte[] buffer = Encoding.UTF8.GetBytes(csvContent.ToString());
            return File(buffer, "text/csv", fileName);
        }

    }
}



