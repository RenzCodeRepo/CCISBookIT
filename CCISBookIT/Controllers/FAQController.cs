using Microsoft.AspNetCore.Mvc;

namespace CCISBookIT.Controllers
{
    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
