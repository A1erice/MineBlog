using Microsoft.AspNetCore.Mvc;
using MineBlog.Models;
using System.Diagnostics;

namespace MineBlog.Controllers
{
    public class UHomeController : Controller
    {
        private readonly ILogger<UHomeController> _logger;

        public UHomeController(ILogger<UHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}