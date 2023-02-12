using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlinePharmacyStore.DataAccess.Repository.IRepository;
using OnlinePharmacyStore.Models;
using OnlinePharmacyStore.Models.CommonPropertyDirectory;

namespace OnlinePharmacyStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InjectionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public InjectionsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Injections
        public IActionResult Index()
        {
            return _unitOfWork.Injection != null ?
                        View(_unitOfWork.Injection.GetAll()) :
                        Problem("Entity set 'AppDbContext.Injections'  is null.");
        }

        // GET: Admin/Injections/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _unitOfWork.Injection == null)
            {
                return NotFound();
            }

            var injection = _unitOfWork.Injection
                .GetFirstOrDefault(m => m.Id == id);
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

        //POST: Admin/Injections/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,GenericName,BrandName,Country,Price,MRPRs,MfgDate,ExpDate,ImageUrl,BatchNo,centigrade,Milliliter")] Injection injection, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file.FileName != null)
                {
                    _unitOfWork.Injection.SaveImage(injection, file, webHostEnvironment.WebRootPath);
                }
                _unitOfWork.Injection.Add(injection);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(injection);
        }

        // GET: Admin/Injections/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _unitOfWork.Injection == null)
            {
                return NotFound();
            }

            var injection = _unitOfWork.Injection.GetFirstOrDefault(u => u.Id == id);
            if (injection == null)
            {
                return NotFound();
            }
            return View(injection);
        }

        //// POST: Admin/Injections/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, [Bind("Id,GenericName,BrandName,Country,Price,MRPRs,MfgDate,ExpDate,ImageUrl,BatchNo,centigrade,Milliliter")] Injection injection, IFormFile file)
        public IActionResult Edit(Injection injection, IFormFile file)
        {
            //if (id != injection.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {

                _unitOfWork.Injection.Update(injection, file, webHostEnvironment.WebRootPath);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(injection);
        }

        //// GET: Admin/Injections/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Injections == null)
        //    {
        //        return NotFound();
        //    }

        //    var injection = await _context.Injections
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (injection == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(injection);
        //}

        //// POST: Admin/Injections/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Injections == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.Injections'  is null.");
        //    }
        //    var injection = await _context.Injections.FindAsync(id);
        //    if (injection != null)
        //    {
        //        _context.Injections.Remove(injection);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool InjectionExists(int id)
        //{
        //  return (_context.Injections?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
