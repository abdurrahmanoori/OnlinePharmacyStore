using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlinePharmacyStore.DataAccess.Data;

namespace StudentGuidence.Areas.Admin.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class ArticleController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _iWebHostEnvironment;

        public ArticleController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _iWebHostEnvironment = webHostEnvironment;
        }

        //GET Create
        public IActionResult Create()
        {
            return View();
        }

        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Article article, IFormFile file)
        {

            article.ImageUrl = file.FileName;

            if (ModelState.IsValid)
            {
                string wwwRootPath = _iWebHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\article");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    article.ImageUrl = @"\images\article\" + fileName + extension;
                }

                _db.Articles.Add(article);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        //GET Edit
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Article article = _db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Article article, IFormFile? file)
        {
            Article article1 = new Article
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                DateOfIssue = article.DateOfIssue,
                ImageUrl = GetImageUrl(article)
            };

            if (ModelState.IsValid)
            {
                string wwwRootPath = _iWebHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\article");
                    var extension = Path.GetExtension(file.FileName);
                    if (article1.ImageUrl != null)
                    {
                        //LOGIC ABOUT DELETING OLD IMAGE
                        var oldImagePath = Path.Combine(wwwRootPath, article1.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    article1.ImageUrl = @"\images\article\" + fileName + extension;
                }

                _db.Articles.Update(article1);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article1);
        }

        //GET Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Article article = _db.Articles.Find(id);
            if (article != null)
            {
                return View(article);
            }
            return NotFound();
        }

        //POST Delete
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Article article = _db.Articles.Find(id);
            string wwwRootPath = _iWebHostEnvironment.WebRootPath;

            if (article.ImageUrl != null)
            {
                //LOGIC ABOUT DELETING OLD IMAGE
                var oldImagePath = Path.Combine(wwwRootPath, article.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _db.Articles.Remove(article);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public string GetImageUrl(Article article)
        {
            using (AppDbContext2 context = new AppDbContext2())
            {
                int id = article.Id;
                return context.Articles.Find(id).ImageUrl;
            }

        }
    }
}