using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlinePharmacyStore.DataAccess.Data;
using OnlinePharmacyStore.Models;

namespace OnlinePharmacyStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InjectionsController : Controller
    {
        private readonly AppDbContext _context;

        public InjectionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Injections
        public async Task<IActionResult> Index()
        {
              return _context.Injections != null ? 
                          View(await _context.Injections.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Injections'  is null.");
        }

        // GET: Admin/Injections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Injections == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (injection == null)
            {
                return NotFound();
            }

            return View(injection);
        }

        // GET: Admin/Injections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Injections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GenericName,BrandName,Country,Price,MRPRs,MfgDate,ExpDate,ImageUrl,BatchNo,centigrade,Milliliter")] Injection injection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(injection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(injection);
        }

        // GET: Admin/Injections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Injections == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections.FindAsync(id);
            if (injection == null)
            {
                return NotFound();
            }
            return View(injection);
        }

        // POST: Admin/Injections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GenericName,BrandName,Country,Price,MRPRs,MfgDate,ExpDate,ImageUrl,BatchNo,centigrade,Milliliter")] Injection injection)
        {
            if (id != injection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(injection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InjectionExists(injection.Id))
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
            return View(injection);
        }

        // GET: Admin/Injections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Injections == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (injection == null)
            {
                return NotFound();
            }

            return View(injection);
        }

        // POST: Admin/Injections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Injections == null)
            {
                return Problem("Entity set 'AppDbContext.Injections'  is null.");
            }
            var injection = await _context.Injections.FindAsync(id);
            if (injection != null)
            {
                _context.Injections.Remove(injection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InjectionExists(int id)
        {
          return (_context.Injections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
