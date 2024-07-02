using CCISBookIT.Data;
using CCISBookIT.Data.Enum;
using CCISBookIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        //Simple Get Request: User/Create
        public IActionResult Signup()
        {
            return View();
        }

       /* [HttpPost]
        public async Task<IActionResult> Signup([Bind("FacultyID,FullName,Email,PhoneNumber")] User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

        }
       */

        public IActionResult Login()
        {
            return View(); 
        }

        public IActionResult ForgotPassword()
        {
            return View(); 
        }

        
        
    }
 }