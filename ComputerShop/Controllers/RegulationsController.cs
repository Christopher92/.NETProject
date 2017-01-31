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
    public class RegulationsController : Controller
    {
        private ComputerShopContext db = new ComputerShopContext();

        // GET: Regulations
        public ActionResult Index()
        {
            return View(db.Regulations.ToList());
        }

        [HttpPost]
        public PartialViewResult Regulation()
        {
            return PartialView(db.Regulations.ToList());
        }

        // GET: Regulations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regulation regulation = db.Regulations.Find(id);
            if (regulation == null)
            {
                return HttpNotFound();
            }
            return View(regulation);
        }

        // GET: Regulations/Create
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: Regulations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Create([Bind(Include = "RegulationID,Rule")] Regulation regulation)
        {
            if (ModelState.IsValid)
            {
                db.Regulations.Add(regulation);
                db.SaveChanges();
                return PartialView("~/Views/Admin/Regulations.cshtml", db.Regulations.ToList());
            }
            return PartialView();
        }

        // GET: Regulations/Edit/5
        public PartialViewResult Edit()
        {

            Regulation regulation = db.Regulations.FirstOrDefault();
            if (regulation == null)
            {
                
            }
            return PartialView(regulation);
        }

        // POST: Regulations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public PartialViewResult Edit([Bind(Include = "RegulationID,Rule")] Regulation regulation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regulation).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("~/Views/Regulation/Edit.cshtml", db.Regulations.ToList());
            }
            else if (regulation.Rule != ""){
                db.Regulations.Add(regulation);
                db.SaveChanges();
                return PartialView("~/Views/Regulation/Edit.cshtml", db.Regulations.ToList());
            }

            return PartialView();
        }

        // GET: Regulations/Delete/5
        public PartialViewResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regulation regulation = db.Regulations.Find(id);
            if (regulation == null)
            {
                //return HttpNotFound();
            }
            return PartialView(regulation);
        }

        // POST: Regulations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public PartialViewResult DeleteConfirmed(int id)
        {
            Regulation regulation = db.Regulations.Find(id);
            db.Regulations.Remove(regulation);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return PartialView("~/Views/Admin/Regulations.cshtml", db.Regulations.ToList());
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
