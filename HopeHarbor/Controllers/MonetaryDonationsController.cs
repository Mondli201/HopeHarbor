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
    public class MonetaryDonationsController : Controller
    {
        private readonly DJContext _context;

        public MonetaryDonationsController(DJContext context)
        {
            _context = context;
        }

        // GET: MonetaryDonations
        public async Task<IActionResult> Index()
        {
              return _context.MonetaryDonation != null ? 
                          View(await _context.MonetaryDonation.ToListAsync()) :
                          Problem("Entity set 'DJContext.MonetaryDonation'  is null.");
        }

        // GET: MonetaryDonations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MonetaryDonation == null)
            {
                return NotFound();
            }

            var monetaryDonation = await _context.MonetaryDonation
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (monetaryDonation == null)
            {
                return NotFound();
            }

            return View(monetaryDonation);
        }

        // GET: MonetaryDonations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MonetaryDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonationId,DonorName,DonationDate,Amount,IsAnonymous,DonationDescription")] MonetaryDonation monetaryDonation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monetaryDonation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monetaryDonation);
        }

        // GET: MonetaryDonations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MonetaryDonation == null)
            {
                return NotFound();
            }

            var monetaryDonation = await _context.MonetaryDonation.FindAsync(id);
            if (monetaryDonation == null)
            {
                return NotFound();
            }
            return View(monetaryDonation);
        }

        // POST: MonetaryDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonationId,DonorName,DonationDate,Amount,IsAnonymous,DonationDescription")] MonetaryDonation monetaryDonation)
        {
            if (id != monetaryDonation.DonationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monetaryDonation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonetaryDonationExists(monetaryDonation.DonationId))
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
            return View(monetaryDonation);
        }

        // GET: MonetaryDonations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MonetaryDonation == null)
            {
                return NotFound();
            }

            var monetaryDonation = await _context.MonetaryDonation
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (monetaryDonation == null)
            {
                return NotFound();
            }

            return View(monetaryDonation);
        }

        // POST: MonetaryDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MonetaryDonation == null)
            {
                return Problem("Entity set 'DJContext.MonetaryDonation'  is null.");
            }
            var monetaryDonation = await _context.MonetaryDonation.FindAsync(id);
            if (monetaryDonation != null)
            {
                _context.MonetaryDonation.Remove(monetaryDonation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonetaryDonationExists(int id)
        {
          return (_context.MonetaryDonation?.Any(e => e.DonationId == id)).GetValueOrDefault();
        }
    }
}
