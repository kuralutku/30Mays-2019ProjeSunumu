using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project4.Data.Abstract;
using Project4.Entity;

namespace Project4.UI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository _blogRepository;
        private ICategoryRepository _categoryRepository;
        public BlogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Details(int id)
        {
            return View(_blogRepository.GetById(id));
        }

        public IActionResult Index(int? id, string q)
        {
            var query = _blogRepository.GetAll().Where(i => i.isApproved);
            if (id != null)
            {
                query = query.Where(i => i.CategoryId == id);
            }

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(i => EF.Functions.Like(i.Title, "%" + q + "%") || EF.Functions.Like(i.Descriptin, "%" + q + "%") || EF.Functions.Like(i.Body, "%" + q + "%"));
            }

            return View(query.OrderByDescending(i => i.Date));
        }

        public IActionResult List()
        {
            return View(_blogRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {

            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");

            return View(new Blog());
        }

        [HttpPost]
        public IActionResult Create(Blog entity)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.SaveBlog(entity);
                TempData["message"] = $"{entity.Title} kayıt edildi.";
                return RedirectToAction("List");
            }

            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");

            return View(entity);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");

            return View(_blogRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Blog entity, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    entity.Image = file.FileName;
                }
                _blogRepository.SaveBlog(entity);
                TempData["message"] = $"{entity.Title} kayıt edildi.";
                return RedirectToAction("List");
            }

            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");

            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_blogRepository.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int BlogId)
        {
            _blogRepository.DeleteBlog(BlogId);
            TempData["message"] = $"{BlogId} numaralı kayıt silindi.";
            return RedirectToAction("List");
        }
    }
}