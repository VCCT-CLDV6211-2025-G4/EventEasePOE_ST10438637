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
    public class BookingsMsController : Controller
    {
        private readonly AppDbContext _context;

        public BookingsMsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BookingsMs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Bookings.Include(b => b.Event).Include(b => b.Venue);
            return View(await appDbContext.ToListAsync());
        }

        // GET: BookingsMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingsM = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookingsM == null)
            {
                return NotFound();
            }

            return View(bookingsM);
        }

        // GET: BookingsMs/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId");
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName");
            return View();
        }

        // POST: BookingsMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,BookingDate,StartDate,EndDate,VenueId,EventId")] BookingsM bookingsM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingsM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", bookingsM.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", bookingsM.VenueId);
            return View(bookingsM);
        }

        // GET: BookingsMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingsM = await _context.Bookings.FindAsync(id);
            if (bookingsM == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", bookingsM.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", bookingsM.VenueId);
            return View(bookingsM);
        }

        // POST: BookingsMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,BookingDate,StartDate,EndDate,VenueId,EventId")] BookingsM bookingsM)
        {
            if (id != bookingsM.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingsM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsMExists(bookingsM.BookingId))
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
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", bookingsM.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", bookingsM.VenueId);
            return View(bookingsM);
        }

        // GET: BookingsMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingsM = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookingsM == null)
            {
                return NotFound();
            }

            return View(bookingsM);
        }

        // POST: BookingsMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingsM = await _context.Bookings.FindAsync(id);
            if (bookingsM != null)
            {
                _context.Bookings.Remove(bookingsM);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsMExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
