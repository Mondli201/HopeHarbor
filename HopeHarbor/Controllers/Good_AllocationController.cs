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
    public class Good_AllocationController : Controller
    {
        private readonly DJContext _context;

        public Good_AllocationController(DJContext context)
        {
            _context = context;
        }

        // GET: Good_Allocation
        public async Task<IActionResult> Index()
        {
            var dJContext = _context.Good_Allocation.Include(g => g.Disaster).Include(g => g.GoodsDonation);
            return View(await dJContext.ToListAsync());
        }

        // GET: Good_Allocation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Good_Allocation == null)
            {
                return NotFound();
            }

            var good_Allocation = await _context.Good_Allocation
                .Include(g => g.Disaster)
                .Include(g => g.GoodsDonation)
                .FirstOrDefaultAsync(m => m.Goods_Id == id);
            if (good_Allocation == null)
            {
                return NotFound();
            }

            return View(good_Allocation);
        }

        // GET: Good_Allocation/Create
        public IActionResult Create()
        {
            ViewData["DisasterId"] = new SelectList(_context.Disaster, "DisasterId", "Location");
            ViewData["DonationId"] = new SelectList(_context.GoodsDonation, "DonationId", "DonorName");
            return View();
        }

        // POST: Good_Allocation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Goods_Id,DisasterId,DonationId,Quantity,Date")] Good_Allocation good_Allocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(good_Allocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisasterId"] = new SelectList(_context.Disaster, "DisasterId", "Location", good_Allocation.DisasterId);
            ViewData["DonationId"] = new SelectList(_context.GoodsDonation, "DonationId", "DonorName", good_Allocation.DonationId);
            return View(good_Allocation);
        }

        // GET: Good_Allocation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Good_Allocation == null)
            {
                return NotFound();
            }

            var good_Allocation = await _context.Good_Allocation.FindAsync(id);
            if (good_Allocation == null)
            {
                return NotFound();
            }
            ViewData["DisasterId"] = new SelectList(_context.Disaster, "DisasterId", "Location", good_Allocation.DisasterId);
            ViewData["DonationId"] = new SelectList(_context.GoodsDonation, "DonationId", "DonorName", good_Allocation.DonationId);
            return View(good_Allocation);
        }

        // POST: Good_Allocation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Goods_Id,DisasterId,DonationId,Quantity,Date")] Good_Allocation good_Allocation)
        {
            if (id != good_Allocation.Goods_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(good_Allocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Good_AllocationExists(good_Allocation.Goods_Id))
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
            ViewData["DisasterId"] = new SelectList(_context.Disaster, "DisasterId", "Location", good_Allocation.DisasterId);
            ViewData["DonationId"] = new SelectList(_context.GoodsDonation, "DonationId", "DonorName", good_Allocation.DonationId);
            return View(good_Allocation);
        }

        // GET: Good_Allocation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Good_Allocation == null)
            {
                return NotFound();
            }

            var good_Allocation = await _context.Good_Allocation
                .Include(g => g.Disaster)
                .Include(g => g.GoodsDonation)
                .FirstOrDefaultAsync(m => m.Goods_Id == id);
            if (good_Allocation == null)
            {
                return NotFound();
            }

            return View(good_Allocation);
        }

        // POST: Good_Allocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Good_Allocation == null)
            {
                return Problem("Entity set 'DJContext.Good_Allocation'  is null.");
            }
            var good_Allocation = await _context.Good_Allocation.FindAsync(id);
            if (good_Allocation != null)
            {
                _context.Good_Allocation.Remove(good_Allocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Good_AllocationExists(int id)
        {
          return (_context.Good_Allocation?.Any(e => e.Goods_Id == id)).GetValueOrDefault();
        }
    }
}
