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
        private readonly EmailService _emailService;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context, EmailService emailService)
        {
            _emailService = emailService;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
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
                        return RedirectToAction("Index", "Home");
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
                EmailConfirmed = true,
                PhoneNumber = registerViewModel.PhoneNumber,
                UserName = registerViewModel.FullName.Replace(" ", "")
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost()]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return RedirectToAction("ForgotPasswordConfirmation", "Account");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            await _emailService.SendEmailAsync(
                user.Email,
                "Reset Password",
                $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>."
            );

            return RedirectToAction("ForgotPasswordConfirmation", "Account");
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string token = null)
        {
            if (token == null)
            {
                return BadRequest("A token must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Token = token };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation" , "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
