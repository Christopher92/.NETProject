using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ComputerShop.DAL;
using ComputerShop.Models;
using PagedList;

namespace ComputerShop.Controllers
{
    public class CategoriesController : Controller
    {
        private ComputerShopContext db = new ComputerShopContext();

        [HttpPost]
        public PartialViewResult Menu()
        {
            var categories = db.Categories.Include(c => c.ParentCategory).Where(x => x.Enable == true);
            return PartialView(categories.ToList());
        }

        [HttpGet]
        public PartialViewResult Menu(int? id, int Page = 1, int pageSize = 5)
        {
            var Categories = db.Categories.Include(c => c.ParentCategory).Where(x => x.Enable == true).ToList();
            var Products = db.Products.Where(x => x.CategoryID==id).OrderBy(x => x.Name).ToPagedList(Page, pageSize);

            var tuple = new Tuple<List<Category>, IPagedList<Product>>(Categories, Products);

            return PartialView("MenuSearch", tuple);
        }


        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.ParentCategory).Where(x => x.Enable == true);
            return View(categories.ToList());
        }

        public PartialViewResult IndexForAjax()
        {
            var categories = db.Categories.Include(c => c.ParentCategory).Where(x => x.Enable == true);
            return PartialView(categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            //category.
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public void Activate(int id)
        {
            Category category = db.Categories.Find(id);
            category.Enable = !category.Enable;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
        }

        [HttpPost]
        public PartialViewResult List(int? id, int Page = 1, int pageSize = 5)
        {
            if (id == null)
            {
                id = 0;
            }
            IOrderedEnumerable<Product> products;
            IOrderedQueryable<Product> products2;

            Category category = db.Categories.Find(id);
            var prod = db.Products.Where(x => x.Category.ParentCategoryID == id);
            if (id != 0)
            {
                products = category.Products.Union(db.Products.Where(x => x.Category.ParentCategoryID == id)).ToList().Where(x => x.Category.Enable == true).Where(x => x.Enable == true).OrderBy(x => x.Name);
                return PartialView(products.ToPagedList(Page, pageSize));
            }
            else
            {
                products2 = db.Products.Where(x => x.Enable == true).Where(x => x.Category.Enable == true).OrderBy(x => x.ProductID);
                return PartialView(products2.ToPagedList(Page, pageSize));
            }
            //if (category == null)
            //{
            //    //return HttpNotFound();
            //}
        }

        [HttpPost]
        public PartialViewResult Search(string searchString, int Page = 1, int pageSize = 5)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var Categories = db.Categories.Include(c => c.ParentCategory).Where(x => x.Enable == true).ToList();
                var Products = db.Products.Where(x => x.Name.Contains(searchString)).OrderBy(x => x.Name).ToPagedList(Page, pageSize);

                var tuple = new Tuple<List<Category>, IPagedList<Product>>(Categories,Products);
                return PartialView("MenuSearch",tuple);
            }
            else
                return PartialView(null);
        }

        // GET: Categories/Create
        public PartialViewResult Create()
        {
            ViewBag.ParentCategoryID = new SelectList(db.Categories.Where(x => x.ParentCategory == null), "CategoryID", "Name");
            return PartialView();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Create([Bind(Include = "CategoryID,Enable,ParentCategoryID,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return PartialView("~/Views/Admin/Categories.cshtml", db.Categories.ToList());
            }
            ViewBag.ParentCategoryID = new SelectList(db.Categories, "CategoryID", "Name", category.ParentCategoryID);
            return PartialView();
        }

        // GET: Categories/Edit/5
        public PartialViewResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                //return HttpNotFound();
            }
            ViewBag.ParentCategoryID = new SelectList(db.Categories, "CategoryID", "Name", category.ParentCategoryID);
            return PartialView(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Edit([Bind(Include = "CategoryID,Enable,ParentCategoryID,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("~/Views/Admin/Categories.cshtml", db.Categories.ToList());

            }
            ViewBag.ParentCategoryID = new SelectList(db.Categories, "CategoryID", "Name", category.ParentCategoryID);
            return PartialView();
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
