﻿using CCISBookIT.Data;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using CCISBookIT.Services_and_Interfaces.Services;
using CCISBookIT.ViewModels;
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
        private readonly UserManager<AppUser> _userManager;


        // Constructor injection of IUserService
        public UserController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        // GET: User/Index
        public async Task<IActionResult> Index()
        {
            var users = (await _userService.GetAllUsersAsync()); // Retrieve and sort users by FacultyID

            var sortedUsers = users.OrderBy(u => u.FacultyID).ToList(); // Sort users by FacultyID

            return View(sortedUsers); // Pass sorted users to the "Index" view
        }

        // GET: User/Detail/{facultyId}
        [HttpGet("User/Detail/{facultyId}")]
        public async Task<IActionResult> Detail(string facultyId)
        {
            var user = await _userManager.FindByFacultyIDAsync(facultyId);

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



        // GET: User/Create
        public IActionResult Create()
        {
            var newUser = new AppUser
            {
                /*Role = "Faculty"*/ // Set default role to "Faculty"
            };
            return View(newUser); // Return create view with a new user instance
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacultyID, FullName, Email, PhoneNumber, PasswordHash, Role")] AppUser user)
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

            //if (string.IsNullOrEmpty(user.Role))
            //{
            //    user.Role = "Faculty"; // Set default role if not provided
            //}

            user.PasswordHash = user.PasswordHash; // Hash the user's password
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
        public async Task<IActionResult> DeleteConfirmed(string facultyId)
        {
            await _userService.Delete(facultyId);
            return RedirectToAction(nameof(Index));
        }
    }
}



