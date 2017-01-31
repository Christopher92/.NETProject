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

namespace ComputerShop.Controllers
{
    public class ProductsController : Controller
    {
        private ComputerShopContext db = new ComputerShopContext();

        // GET: Products
        [HttpPost]
        public PartialViewResult Index()
        {
            var products = db.Products.Include(p => p.Category).Where(p => p.Category.Enable==true);
            return PartialView(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public PartialViewResult RetrieveProduct(int? id)
        {
            Product product = db.Products.Find(id);
            return PartialView(product);
        }

        public void Activate(int id)
        {
            Product product= db.Products.Find(id);
            product.Enable = !product.Enable;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
        }

        // GET: Products/Create
        public PartialViewResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return PartialView();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductPicture PP = new ProductPicture {Path=product.ProductPicture.Path };
                db.ProductPictures.Add(PP);
                db.Products.Add(product);
                db.SaveChanges();
                return PartialView("~/Views/Admin/Products.cshtml", db.Products.ToList());
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return PartialView();
        }

        // GET: Products/Edit/5
        public PartialViewResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                //return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            return PartialView(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Edit([Bind(Include = "ProductID,CategoryID,Name,Price,Description,Quantity,Enable")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("~/Views/Admin/Products.cshtml", db.Products.ToList());
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            return PartialView();
        }

        [HttpPost]
        public void AddProducts(int productId, int quantity)
        {
            if (quantity > 0)
            {
                Product product = db.Products.Find(productId);
                product.Quantity += quantity;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
