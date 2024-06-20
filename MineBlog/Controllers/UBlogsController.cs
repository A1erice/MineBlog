using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MineBlog.Data;
using MineBlog.Models;

namespace MineBlog.Controllers
{
    public class UBlogsController : Controller
    {
        private readonly MineBlogDbContext _context;

        public UBlogsController(MineBlogDbContext context)
        {
            _context = context;
        }

        // GET: ABlogs
        public async Task<IActionResult> Index()
        {
            var mineBlogDbContext = _context.Blog
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Tags);
            return View(await mineBlogDbContext.ToListAsync());
        }
        // GET: ABlogs
        public async Task<IActionResult> List(int categoryId)
        {
            var mineBlogDbContext = _context.Blog
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Tags)
                .Where(b => b.CategoryId == categoryId);
            var category = await _context.Category.FindAsync(categoryId);
            ViewBag.CategoryName = category.Name;
            ViewBag.CategoryURL = category.ImageUrl;
            return View(await mineBlogDbContext.ToListAsync());
        }
        // GET: ABlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Username");
            ViewBag.CurrentBlogId = id;
            if (id == null || _context.Blog == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Tags)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewBag.CategoryID = blog.Category.Id;
            return View(blog);
        }
    }
}
