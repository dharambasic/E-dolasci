using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Studenti.Data;
using Studenti.Models;

namespace Studenti.Controllers
{
    public class KolegijisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KolegijisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kolegijis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kolegiji.ToListAsync());
        }

        // GET: Kolegijis/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kolegiji = await _context.Kolegiji
                .FirstOrDefaultAsync(m => m.kolegijID == id);
            if (kolegiji == null)
            {
                return NotFound();
            }

            return View(kolegiji);
        }

        // GET: Kolegijis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kolegijis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("kolegijID,kolegijIme,kolegijGodinaID")] Kolegiji kolegiji)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kolegiji);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kolegiji);
        }

        // GET: Kolegijis/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kolegiji = await _context.Kolegiji.FindAsync(id);
            if (kolegiji == null)
            {
                return NotFound();
            }
            return View(kolegiji);
        }

        // POST: Kolegijis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("kolegijID,kolegijIme,kolegijGodinaID")] Kolegiji kolegiji)
        {
            if (id != kolegiji.kolegijID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kolegiji);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KolegijiExists(kolegiji.kolegijID))
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
            return View(kolegiji);
        }

        // GET: Kolegijis/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kolegiji = await _context.Kolegiji
                .FirstOrDefaultAsync(m => m.kolegijID == id);
            if (kolegiji == null)
            {
                return NotFound();
            }

            return View(kolegiji);
        }

        // POST: Kolegijis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var kolegiji = await _context.Kolegiji.FindAsync(id);
            _context.Kolegiji.Remove(kolegiji);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KolegijiExists(string id)
        {
            return _context.Kolegiji.Any(e => e.kolegijID == id);
        }
    }
}
