using CCISBookIT.Data;
using System.Threading.Tasks;
using CCISBookIT.Models;
using CCISBookIT.Services_and_Interfaces.Services;
using CCISBookIT.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CCISBookIT.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            AppUser user = await _userManager.FindByFacultyIDAsync(loginViewModel.FacultyID);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Room");
                    }
                }
                TempData["Error"] = "Incorrect Password. Try Again";
                return View(loginViewModel);
            }
            TempData["Error"] = "User not Found. Try Again";
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            var response = new LoginViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByFacultyIDAsync(registerViewModel.FacultyID);
            if (user != null)
            {
                TempData["Error"] = "This Faculty ID is already in use.";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                FacultyID = registerViewModel.FacultyID,
                FullName = registerViewModel.FullName,
                Email = registerViewModel.Email,
                PhoneNumber = registerViewModel.PhoneNumber,
                UserName = registerViewModel.FullName.Replace(" ", "")
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return RedirectToAction("Index", "Room");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
