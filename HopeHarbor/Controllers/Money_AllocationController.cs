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
    public class Money_AllocationController : Controller
    {
        private readonly DJContext _context;

        public Money_AllocationController(DJContext context)
        {
            _context = context;
        }

        // GET: Money_Allocation
        public async Task<IActionResult> Index()
        {
            var dJContext = _context.Money_Allocation.Include(m => m.Disaster);
            return View(await dJContext.ToListAsync());
        }

        // GET: Money_Allocation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Money_Allocation == null)
            {
                return NotFound();
            }

            var money_Allocation = await _context.Money_Allocation
                .Include(m => m.Disaster)
                .FirstOrDefaultAsync(m => m.MoneyID == id);
            if (money_Allocation == null)
            {
                return NotFound();
            }

            return View(money_Allocation);
        }

        // GET: Money_Allocation/Create
        public IActionResult Create()
        {
            ViewData["DisasterId"] = new SelectList(_context.Disaster, "DisasterId", "Location");
            return View();
        }

        // POST: Money_Allocation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoneyID,DisasterId,Amount,Date")] Money_Allocation money_Allocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(money_Allocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisasterId"] = new SelectList(_context.Disaster, "DisasterId", "Location", money_Allocation.DisasterId);
            return View(money_Allocation);
        }

        // GET: Money_Allocation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Money_Allocation == null)
            {
                return NotFound();
            }

            var money_Allocation = await _context.Money_Allocation.FindAsync(id);
            if (money_Allocation == null)
            {
                return NotFound();
            }
            ViewData["DisasterId"] = new SelectList(_context.Disaster, "DisasterId", "Location", money_Allocation.DisasterId);
            return View(money_Allocation);
        }

        // POST: Money_Allocation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoneyID,DisasterId,Amount,Date")] Money_Allocation money_Allocation)
        {
            if (id != money_Allocation.MoneyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(money_Allocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Money_AllocationExists(money_Allocation.MoneyID))
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
            ViewData["DisasterId"] = new SelectList(_context.Disaster, "DisasterId", "Location", money_Allocation.DisasterId);
            return View(money_Allocation);
        }

        // GET: Money_Allocation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Money_Allocation == null)
            {
                return NotFound();
            }

            var money_Allocation = await _context.Money_Allocation
                .Include(m => m.Disaster)
                .FirstOrDefaultAsync(m => m.MoneyID == id);
            if (money_Allocation == null)
            {
                return NotFound();
            }

            return View(money_Allocation);
        }

        // POST: Money_Allocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Money_Allocation == null)
            {
                return Problem("Entity set 'DJContext.Money_Allocation'  is null.");
            }
            var money_Allocation = await _context.Money_Allocation.FindAsync(id);
            if (money_Allocation != null)
            {
                _context.Money_Allocation.Remove(money_Allocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Money_AllocationExists(int id)
        {
          return (_context.Money_Allocation?.Any(e => e.MoneyID == id)).GetValueOrDefault();
        }
    }
}
