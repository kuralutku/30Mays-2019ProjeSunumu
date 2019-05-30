using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project4.Data.Abstract;
using Project4.UI.Models;

namespace Project4.UI.Controllers
{
    public class HomeController : Controller
    {
        private IBlogRepository blogRepository;

        public HomeController(IBlogRepository repository)
        {
            blogRepository = repository;
        }

        public IActionResult Index()
        {
            var model = new HomeBlogModel();

            model.HomeBlogs = blogRepository.GetAll().Where(i => i.isApproved && i.isHome).ToList();
            model.SliderBlogs = blogRepository.GetAll().Where(i => i.isApproved && i.isSlider).ToList();

            return View(model);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}