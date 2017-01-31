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
    public class OrderSuppliesController : Controller
    {
        private ComputerShopContext db = new ComputerShopContext();

        // GET: OrderSupplies
        public ActionResult Index()
        {
            return View(db.OrderSupplies.ToList());
        }

        // GET: OrderSupplies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderSupply orderSupply = db.OrderSupplies.Find(id);
            if (orderSupply == null)
            {
                return HttpNotFound();
            }
            return View(orderSupply);
        }

        // GET: OrderSupplies/Create
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: OrderSupplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Create([Bind(Include = "OrderSupplyID,Name,Time,Price")] OrderSupply orderSupply)
        {
            if (ModelState.IsValid)
            {
                db.OrderSupplies.Add(orderSupply);
                db.SaveChanges();
                return PartialView("~/Views/Admin/OrderSupplies.cshtml", db.OrderSupplies.ToList());
            }

            return PartialView();
        }

        // GET: OrderSupplies/Edit/5
        public PartialViewResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderSupply orderSupply = db.OrderSupplies.Find(id);
            if (orderSupply == null)
            {
                //return HttpNotFound();
            }
            return PartialView(orderSupply);
        }

        // POST: OrderSupplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Edit([Bind(Include = "OrderSupplyID,Name,Time,Price")] OrderSupply orderSupply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderSupply).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("~/Views/Admin/OrderSupplies.cshtml", db.OrderSupplies.ToList());
            }
            return PartialView();
        }

        // GET: OrderSupplies/Delete/5
        public PartialViewResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderSupply orderSupply = db.OrderSupplies.Find(id);
            if (orderSupply == null)
            {
                //return HttpNotFound();
            }
            return PartialView(orderSupply);
        }

        // POST: OrderSupplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public PartialViewResult DeleteConfirmed(int id)
        {
            OrderSupply orderSupply = db.OrderSupplies.Find(id);
            db.OrderSupplies.Remove(orderSupply);
            db.SaveChanges();
            return PartialView("~/Views/Admin/OrderSupplies.cshtml", db.OrderSupplies.ToList());
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
