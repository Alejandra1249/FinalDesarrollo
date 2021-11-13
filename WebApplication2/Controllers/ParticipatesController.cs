using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ParticipatesController : Controller
    {
        private readonly WebApplication2Context _context;

        public ParticipatesController(WebApplication2Context context)
        {
            _context = context;
        }

        // GET: Participates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Participate.ToListAsync());
        }

        // GET: Participates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participate = await _context.Participate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participate == null)
            {
                return NotFound();
            }

            return View(participate);
        }

        // GET: Participates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Participates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nombre,dpi,telefono,direccion,correo")] Participate participate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participate);
        }

        // GET: Participates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participate = await _context.Participate.FindAsync(id);
            if (participate == null)
            {
                return NotFound();
            }
            return View(participate);
        }

        // POST: Participates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nombre,dpi,telefono,direccion,correo")] Participate participate)
        {
            if (id != participate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipateExists(participate.Id))
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
            return View(participate);
        }

        // GET: Participates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participate = await _context.Participate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participate == null)
            {
                return NotFound();
            }

            return View(participate);
        }

        // POST: Participates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participate = await _context.Participate.FindAsync(id);
            _context.Participate.Remove(participate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipateExists(int id)
        {
            return _context.Participate.Any(e => e.Id == id);
        }
    }
}
