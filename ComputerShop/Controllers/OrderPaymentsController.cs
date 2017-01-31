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
    public class OrderPaymentsController : Controller
    {
        private ComputerShopContext db = new ComputerShopContext();

        // GET: OrderPayments
        public ActionResult Index()
        {
            return View(db.OrderPayments.ToList());
        }

        // GET: OrderPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPayment orderPayment = db.OrderPayments.Find(id);
            if (orderPayment == null)
            {
                return HttpNotFound();
            }
            return View(orderPayment);
        }

        // GET: OrderPayments/Create
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: OrderPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Create([Bind(Include = "OrderPaymentID,Name,Price")] OrderPayment orderPayment)
        {
            if (ModelState.IsValid)
            {
                db.OrderPayments.Add(orderPayment);
                db.SaveChanges();
                return PartialView("~/Views/Admin/OrderPayments.cshtml", db.OrderPayments.ToList());
            }
            return PartialView();
        }

        // GET: OrderPayments/Edit/5
        public PartialViewResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPayment orderPayment = db.OrderPayments.Find(id);
            if (orderPayment == null)
            {
                //return HttpNotFound();
            }
            return PartialView(orderPayment);
        }

        // POST: OrderPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Edit([Bind(Include = "OrderPaymentID,Name,Price")] OrderPayment orderPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderPayment).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("~/Views/Admin/OrderPayments.cshtml", db.OrderPayments.ToList());
            }
            return PartialView();
        }

        // GET: OrderPayments/Delete/5
        public PartialViewResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPayment orderPayment = db.OrderPayments.Find(id);
            if (orderPayment == null)
            {
                //return HttpNotFound();
            }
            return PartialView(orderPayment);
        }

        // POST: OrderPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public PartialViewResult DeleteConfirmed(int id)
        {
            OrderPayment orderPayment = db.OrderPayments.Find(id);
            db.OrderPayments.Remove(orderPayment);
            db.SaveChanges();
            return PartialView("~/Views/Admin/OrderPayments.cshtml", db.OrderPayments.ToList());
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
