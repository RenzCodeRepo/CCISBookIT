using CCISBookIT.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCISBookIT.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public  IActionResult Index()
        {
            var user = _context.Users.OrderBy(u => u.FacultyID).ToList(); 
            return View(user);
        }
    }
}
