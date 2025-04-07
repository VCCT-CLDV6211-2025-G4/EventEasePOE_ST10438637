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
    public class EventsMsController : Controller
    {
        private readonly AppDbContext _context;

        public EventsMsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EventsMs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        // GET: EventsMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventsM = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventsM == null)
            {
                return NotFound();
            }

            return View(eventsM);
        }

        // GET: EventsMs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventsMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,StartDate,EndDate,ImageUrl")] EventsM eventsM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventsM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventsM);
        }

        // GET: EventsMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventsM = await _context.Events.FindAsync(id);
            if (eventsM == null)
            {
                return NotFound();
            }
            return View(eventsM);
        }

        // POST: EventsMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,StartDate,EndDate,ImageUrl")] EventsM eventsM)
        {
            if (id != eventsM.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventsM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsMExists(eventsM.EventId))
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
            return View(eventsM);
        }

        // GET: EventsMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventsM = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventsM == null)
            {
                return NotFound();
            }

            return View(eventsM);
        }

        // POST: EventsMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventsM = await _context.Events.FindAsync(id);
            if (eventsM != null)
            {
                _context.Events.Remove(eventsM);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsMExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
