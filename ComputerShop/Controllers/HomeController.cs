using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ComputerShop.DAL;
using ComputerShop.Models;

namespace ComputerShop.Controllers
{
    public class HomeController : Controller
    {
        private ComputerShopContext db = new ComputerShopContext();

        // GET: Categories
        public PartialViewResult Index()
        {
            var categories = db.Categories.Include(c => c.ParentCategory).Where(x => x.Products.Count!=0);
            return PartialView(categories.ToList());
        }


        public ViewResult IndexForAjax()
        {
            var categories = db.Categories.Include(c => c.ParentCategory).Where(x => x.Products.Count != 0);
            return View("IndexForAjax",categories.ToList());
        }


    }
}