using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MineBlog.Data;
using MineBlog.Models;
using System.Diagnostics;

namespace MineBlog.Controllers
{
    public class ULoginController : Controller
    {
        private readonly MineBlogDbContext _dbContext;
        public ULoginController(MineBlogDbContext dbContext) { _dbContext = dbContext; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {

            return RedirectToAction("Index", "Home");
        }
    }
}