﻿using System;
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
    public class ATagsController : Controller
    {
        private readonly MineBlogDbContext _context;

        public ATagsController(MineBlogDbContext context)
        {
            _context = context;
        }

        // GET: ATags
        public async Task<IActionResult> Index()
        {
              return _context.Tag != null ? 
                          View(await _context.Tag.ToListAsync()) :
                          Problem("Entity set 'MineBlogDbContext.Tag'  is null.");
        }

        // GET: ATags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tag == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: ATags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ATags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Tag tag)
        {
            if (true)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: ATags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tag == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: ATags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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
            return View(tag);
        }

        // GET: ATags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tag == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: ATags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tag == null)
            {
                return Problem("Entity set 'MineBlogDbContext.Tag'  is null.");
            }
            var tag = await _context.Tag.FindAsync(id);
            if (tag != null)
            {
                _context.Tag.Remove(tag);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
          return (_context.Tag?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
