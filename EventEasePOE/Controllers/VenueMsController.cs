using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventEasePOE.Data;
using EventEasePOE.Models;

namespace EventEasePOE.Controllers
{
    public class VenueMsController : Controller
    {
        private readonly AppDbContext _context;

        public VenueMsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: VenueMs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Venues.ToListAsync());
        }

        // GET: VenueMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueM = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueId == id);
            if (venueM == null)
            {
                return NotFound();
            }

            return View(venueM);
        }

        // GET: VenueMs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VenueMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueId,VenueName,Location,Capacity,ImageUrl")] VenueM venueM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venueM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venueM);
        }

        // GET: VenueMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueM = await _context.Venues.FindAsync(id);
            if (venueM == null)
            {
                return NotFound();
            }
            return View(venueM);
        }

        // POST: VenueMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueId,VenueName,Location,Capacity,ImageUrl")] VenueM venueM)
        {
            if (id != venueM.VenueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venueM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueMExists(venueM.VenueId))
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
            return View(venueM);
        }

        // GET: VenueMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueM = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueId == id);
            if (venueM == null)
            {
                return NotFound();
            }

            return View(venueM);
        }

        // POST: VenueMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venueM = await _context.Venues.FindAsync(id);
            if (venueM != null)
            {
                _context.Venues.Remove(venueM);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueMExists(int id)
        {
            return _context.Venues.Any(e => e.VenueId == id);
        }
    }
}
