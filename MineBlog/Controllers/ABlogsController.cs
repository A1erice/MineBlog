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
    public class ABlogsController : Controller
    {
        private readonly MineBlogDbContext _context;

        public ABlogsController(MineBlogDbContext context)
        {
            _context = context;
        }

        // GET: ABlogs
        public async Task<IActionResult> Index()
        {
            var mineBlogDbContext = _context.Blog.Include(b => b.Author).Include(b => b.Category);
            return View(await mineBlogDbContext.ToListAsync());
        }

        // GET: ABlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Blog == null)
            {
                return NotFound();
            }
            var blog = await _context.Blog
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Tags)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: ABlogs/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Account, "Id", "Username");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name"); 
            ViewBag.Tags = _context.Tag.ToList();
            return View();
        }

        // POST: ABlogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog, IFormFile image, int[] tags)
        {
            if (true)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                // Save the uploaded image to the wwwroot/images folder
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Blogs", fileName);
                var stream = new FileStream(filePath, FileMode.Create);
                image.CopyToAsync(stream);

                foreach (var tagId in tags)
                {
                    var tag = _context.Tag.Find(tagId);
                    if (tag != null)
                    {
                        blog.Tags.Add(tag);
                    }
                }

                // Update the ImageUrl property with the relative path to the saved image
                blog.ImageUrl = $"/img/Blogs/{fileName}";
                blog.Date = DateTime.Now;
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Account, "Id", "Email", blog.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", blog.CategoryId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Name");
            return View(blog);
        }

        // GET: ABlogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Blog == null)
            {
                return NotFound();
            }
            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewBag.Tags = _context.Tag.ToList();
            ViewData["AuthorId"] = new SelectList(_context.Account, "Id", "Email", blog.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", blog.CategoryId);
            return View(blog);
        }

        // POST: ABlogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,UrlSlug,Content,ImageUrl,Date,CategoryId,AuthorId")] Blog blog, int[] tags)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    var existingBlog = await _context.Blog
                        .Include(b => b.Tags)
                        .FirstOrDefaultAsync(b => b.Id == id);

                    if (existingBlog == null)
                    {
                        return NotFound();
                    }

                    // Update tags
                    existingBlog.Tags.Clear();

                    if (tags != null && tags.Any())
                    {
                        foreach (var tagId in tags.Distinct())
                        {
                            var tag = await _context.Tag.FindAsync(tagId);
                            if (tag != null)
                            {
                                existingBlog.Tags.Add(tag);
                            }
                        }
                    }
                    _context.Update(existingBlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // Reload all tags and pass them to the view
            ViewBag.Tags = _context.Tag.ToList();
            ViewData["AuthorId"] = new SelectList(_context.Account, "Id", "Email", blog.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", blog.CategoryId);
            return View(blog);
        }

        // GET: ABlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Blog == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: ABlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Blog == null)
            {
                return Problem("Entity set 'MineBlogDbContext.Blog'  is null.");
            }
            var blog = await _context.Blog.FindAsync(id);
            if (blog != null)
            {
                _context.Blog.Remove(blog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
          return (_context.Blog?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
