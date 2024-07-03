using CCISBookIT.Data;
using CCISBookIT.Services_and_Interfaces.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CCISBookIT.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService; // Constructor injection
        }

        public async Task<IActionResult> Index()
        {
            var users = (await _userService.GetAll()).OrderBy(u => u.FacultyID).ToList(); // Retrieve and sort users by FacultyID

            return View(users); // Pass sorted users to the "Index" view
        }
    }
}
