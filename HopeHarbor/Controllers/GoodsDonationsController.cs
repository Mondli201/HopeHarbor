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
    public class GoodsDonationsController : Controller
    {
        private readonly DJContext _context;

        public GoodsDonationsController(DJContext context)
        {
            _context = context;
        }

        // GET: GoodsDonations
        public async Task<IActionResult> Index()
        {
            var dJContext = _context.GoodsDonation.Include(g => g.GoodsCategory);
            return View(await dJContext.ToListAsync());
        }

        // GET: GoodsDonations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GoodsDonation == null)
            {
                return NotFound();
            }

            var goodsDonation = await _context.GoodsDonation
                .Include(g => g.GoodsCategory)
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (goodsDonation == null)
            {
                return NotFound();
            }

            return View(goodsDonation);
        }

        // GET: GoodsDonations/Create
        public IActionResult Create()
        {
            ViewData["CategoryName"] = new SelectList(_context.GoodsCategory, "CategoryName", "CategoryName");
            return View();
        }

        // POST: GoodsDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonationId,DonorName,DonationDate,ItemCategory,ItemDescription,ItemCount,IsAnonymous,CategoryName")] GoodsDonation goodsDonation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(goodsDonation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryName"] = new SelectList(_context.GoodsCategory, "CategoryName", "CategoryName", goodsDonation.CategoryName);
            return View(goodsDonation);
        }

        // GET: GoodsDonations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GoodsDonation == null)
            {
                return NotFound();
            }

            var goodsDonation = await _context.GoodsDonation.FindAsync(id);
            if (goodsDonation == null)
            {
                return NotFound();
            }
            ViewData["CategoryName"] = new SelectList(_context.GoodsCategory, "CategoryName", "CategoryName", goodsDonation.CategoryName);
            return View(goodsDonation);
        }

        // POST: GoodsDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonationId,DonorName,DonationDate,ItemCategory,ItemDescription,ItemCount,IsAnonymous,CategoryName")] GoodsDonation goodsDonation)
        {
            if (id != goodsDonation.DonationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goodsDonation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodsDonationExists(goodsDonation.DonationId))
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
            ViewData["CategoryName"] = new SelectList(_context.GoodsCategory, "CategoryName", "CategoryName", goodsDonation.CategoryName);
            return View(goodsDonation);
        }

        // GET: GoodsDonations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GoodsDonation == null)
            {
                return NotFound();
            }

            var goodsDonation = await _context.GoodsDonation
                .Include(g => g.GoodsCategory)
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (goodsDonation == null)
            {
                return NotFound();
            }

            return View(goodsDonation);
        }

        // POST: GoodsDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GoodsDonation == null)
            {
                return Problem("Entity set 'DJContext.GoodsDonation'  is null.");
            }
            var goodsDonation = await _context.GoodsDonation.FindAsync(id);
            if (goodsDonation != null)
            {
                _context.GoodsDonation.Remove(goodsDonation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodsDonationExists(int id)
        {
          return (_context.GoodsDonation?.Any(e => e.DonationId == id)).GetValueOrDefault();
        }
    }
}
