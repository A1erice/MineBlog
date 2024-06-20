using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using MineBlog.Models;
using System.Diagnostics;

namespace MineBlog.Controllers
{
    public class USignupController : Controller
    {
        private readonly ILogger<ULoginController> _logger;
        public USignupController(ILogger<ULoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup()
        {
            return View();
        }
    }
}