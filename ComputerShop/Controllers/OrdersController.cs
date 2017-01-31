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
    public class OrdersController : Controller
    {
        private ComputerShop.DAL.ComputerShopContext db = new ComputerShop.DAL.ComputerShopContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.OrderPayment).Include(o => o.OrderState).Include(o => o.OrderSupply).Include(o => o.User);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.OrderPaymentID = new SelectList(db.OrderPayments, "OrderPaymentID", "Name");
            ViewBag.OrderStateID = new SelectList(db.OrderStates, "OrderStateID", "Name");
            ViewBag.OrderSupplyID = new SelectList(db.OrderSupplies, "OrderSupplyID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                double suma = 0;
                var orderProducts = db.OrderProducts.Where(x => x.OrderID == order.OrderID);
                foreach (var OP in orderProducts)
                {
                    suma += OP.Suma;
                }

                order.Suma = suma;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderPaymentID = new SelectList(db.OrderPayments, "OrderPaymentID", "Name", order.OrderPaymentID);
            ViewBag.OrderStateID = new SelectList(db.OrderStates, "OrderStateID", "Name", order.OrderStateID);
            ViewBag.OrderSupplyID = new SelectList(db.OrderSupplies, "OrderSupplyID", "Name", order.OrderSupplyID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", order.UserID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderPaymentID = new SelectList(db.OrderPayments, "OrderPaymentID", "Name", order.OrderPaymentID);
            ViewBag.OrderStateID = new SelectList(db.OrderStates, "OrderStateID", "Name", order.OrderStateID);
            ViewBag.OrderSupplyID = new SelectList(db.OrderSupplies, "OrderSupplyID", "Name", order.OrderSupplyID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", order.UserID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,UserID,OrderStateID,OrderPaymentID,OrderSupplyID,IncomingDate,OutcomingDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderPaymentID = new SelectList(db.OrderPayments, "OrderPaymentID", "Name", order.OrderPaymentID);
            ViewBag.OrderStateID = new SelectList(db.OrderStates, "OrderStateID", "Name", order.OrderStateID);
            ViewBag.OrderSupplyID = new SelectList(db.OrderSupplies, "OrderSupplyID", "Name", order.OrderSupplyID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", order.UserID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
