using Microsoft.AspNetCore.Mvc;
using CCISBookIT.Models;
using CCISBookIT.Services;
using CCISBookIT.Data;
using Microsoft.AspNetCore.Identity;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordService _passwordService;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
        _passwordService = new PasswordService();
    }

    [HttpPost]
    public IActionResult Create(User user, string password)
    {
        // Hash the password before saving
        user.PasswordHash = _passwordService.HashPassword(password);

        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Login(string facultyId, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.FacultyID == facultyId);
        if (user != null)
        {
            var result = _passwordService.VerifyHashedPassword(user.PasswordHash, password);
            if (result == PasswordVerificationResult.Success)
            {
                // Password is correct
                return RedirectToAction("Index");
            }
        }

        // Invalid login attempt
        ModelState.AddModelError("", "Invalid Faculty ID or password.");
        return View();
    }
}
