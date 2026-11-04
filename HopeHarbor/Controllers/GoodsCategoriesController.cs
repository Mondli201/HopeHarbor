using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HopeHarbor.Data;
using HopeHarbor.Models;
using Microsoft.AspNetCore.Authorization;

namespace HopeHarbor.Controllers
{
    [Authorize]
    public class GoodsCategoriesController : Controller
    {
        private readonly DJContext _context;

        public GoodsCategoriesController(DJContext context)
        {
            _context = context;
        }

        // GET: GoodsCategories
        public async Task<IActionResult> Index()
        {
              return _context.GoodsCategory != null ? 
                          View(await _context.GoodsCategory.ToListAsync()) :
                          Problem("Entity set 'DJContext.GoodsCategory'  is null.");
        }

        // GET: GoodsCategories/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.GoodsCategory == null)
            {
                return NotFound();
            }

            var goodsCategory = await _context.GoodsCategory
                .FirstOrDefaultAsync(m => m.CategoryName == id);
            if (goodsCategory == null)
            {
                return NotFound();
            }

            return View(goodsCategory);
        }

        // GET: GoodsCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GoodsCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryName")] GoodsCategory goodsCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(goodsCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goodsCategory);
        }

        // GET: GoodsCategories/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.GoodsCategory == null)
            {
                return NotFound();
            }

            var goodsCategory = await _context.GoodsCategory.FindAsync(id);
            if (goodsCategory == null)
            {
                return NotFound();
            }
            return View(goodsCategory);
        }

        // POST: GoodsCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CategoryName")] GoodsCategory goodsCategory)
        {
            if (id != goodsCategory.CategoryName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goodsCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodsCategoryExists(goodsCategory.CategoryName))
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
            return View(goodsCategory);
        }

        // GET: GoodsCategories/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.GoodsCategory == null)
            {
                return NotFound();
            }

            var goodsCategory = await _context.GoodsCategory
                .FirstOrDefaultAsync(m => m.CategoryName == id);
            if (goodsCategory == null)
            {
                return NotFound();
            }

            return View(goodsCategory);
        }

        // POST: GoodsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.GoodsCategory == null)
            {
                return Problem("Entity set 'DJContext.GoodsCategory'  is null.");
            }
            var goodsCategory = await _context.GoodsCategory.FindAsync(id);
            if (goodsCategory != null)
            {
                _context.GoodsCategory.Remove(goodsCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodsCategoryExists(string id)
        {
          return (_context.GoodsCategory?.Any(e => e.CategoryName == id)).GetValueOrDefault();
        }
    }
}
