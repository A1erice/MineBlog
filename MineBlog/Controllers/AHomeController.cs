using Microsoft.AspNetCore.Mvc;

namespace MineBlog.Controllers
{
    public class AHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
