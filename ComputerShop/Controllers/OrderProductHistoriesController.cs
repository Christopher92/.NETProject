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
using Microsoft.AspNet.Identity;

namespace ComputerShop.Controllers
{
    public class OrderProductHistoriesController : Controller
    {
        private ComputerShopContext db = new ComputerShopContext();

        [Authorize]
        public PartialViewResult Index()
        {

            string userId = User.Identity.GetUserId();
            var orderProductsHistory = db.OrderProductHistory.Include(o => o.OrderProduct).Where(y => y.OrderProduct.userID == userId);
            return PartialView(orderProductsHistory.ToList());
        }

        // GET: OrderProductHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProductHistory orderProductHistory = db.OrderProductHistory.Find(id);
            if (orderProductHistory == null)
            {
                return HttpNotFound();
            }
            return View(orderProductHistory);
        }

        // GET: OrderProductHistories/Create
        public ActionResult Create()
        {
            ViewBag.OrderProductID = new SelectList(db.OrderProducts, "OrderProductID", "userID");
            return View();
        }

        // POST: OrderProductHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderProductHistoryID,OrderProductID,Price")] OrderProductHistory orderProductHistory)
        {
            if (ModelState.IsValid)
            {
                db.OrderProductHistory.Add(orderProductHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderProductID = new SelectList(db.OrderProducts, "OrderProductID", "userID", orderProductHistory.OrderProductID);
            return View(orderProductHistory);
        }

        // GET: OrderProductHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProductHistory orderProductHistory = db.OrderProductHistory.Find(id);
            if (orderProductHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderProductID = new SelectList(db.OrderProducts, "OrderProductID", "userID", orderProductHistory.OrderProductID);
            return View(orderProductHistory);
        }

        // POST: OrderProductHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderProductHistoryID,OrderProductID,Price")] OrderProductHistory orderProductHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderProductHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderProductID = new SelectList(db.OrderProducts, "OrderProductID", "userID", orderProductHistory.OrderProductID);
            return View(orderProductHistory);
        }

        // GET: OrderProductHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProductHistory orderProductHistory = db.OrderProductHistory.Find(id);
            if (orderProductHistory == null)
            {
                return HttpNotFound();
            }
            return View(orderProductHistory);
        }

        // POST: OrderProductHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderProductHistory orderProductHistory = db.OrderProductHistory.Find(id);
            db.OrderProductHistory.Remove(orderProductHistory);
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
