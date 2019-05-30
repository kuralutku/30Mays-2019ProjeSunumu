using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project4.Data.Abstract;
using Project4.Entity;

namespace Project4.UI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository repository;

        public CategoryController(ICategoryRepository _repository)
        {
            repository = _repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(repository.GetAll());
        }

        [HttpGet]
        public IActionResult AddOrUpdate(int? id)
        {
            if (id == null)
            {
                return View(new Category());
            }
            else
            {
                return View(repository.GetById((int)id));
            }
        }

        [HttpPost]
        public IActionResult AddOrUpdate(Category entity)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCategory(entity);
                TempData["message"] = $"{entity.Name} kayıt edildi.";
                return RedirectToAction("List");
            }
            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(repository.GetById(id));
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int CategoryId)
        {
            repository.DeleteCategory(CategoryId);
            TempData["message"] = $"{CategoryId} numaralı kategori silindi.";
            return RedirectToAction("List");
        }
    }
}