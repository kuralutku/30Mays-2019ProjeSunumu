using Microsoft.AspNetCore.Mvc;
using Project4.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project4.UI.ViewComponents
{
    public class CategoryMenuViewComponent:ViewComponent
    {
        private ICategoryRepository _repository;

        public CategoryMenuViewComponent(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["id"];
            return View(_repository.GetAll());
        }
    }
}
