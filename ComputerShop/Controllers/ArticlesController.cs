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
    public class ArticlesController : Controller
    {
        private ComputerShopContext db = new ComputerShopContext();

        // GET: Articles
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        [HttpPost]
        public PartialViewResult Article(int Page = 1, int pageSize = 5)
        {
            return PartialView(db.Articles.Where(x => x.Enable==true).OrderBy(x => x.ArticleID).ToPagedList(Page, pageSize));
        }

        [HttpPost]
        public PartialViewResult RetrieveArticle(int? id)
        {
            Article article = db.Articles.Find(id);
            return PartialView(article);
        }
        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public void Activate(int id)
        {
            Article article = db.Articles.Find(id);
            article.Enable = !article.Enable;
            db.Entry(article).State = EntityState.Modified;
            db.SaveChanges();
        }

        // GET: Articles/Create
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Create([Bind(Include = "ArticleID,Title,Content,Author,PublicationDate, Enable")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return PartialView("~/Views/Admin/Articles.cshtml", db.Articles.ToList());
            }
            return PartialView();
        }

        // GET: Articles/Edit/5
        public PartialViewResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                //return HttpNotFound();
            }
            return PartialView(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Edit([Bind(Include = "ArticleID,Title,Content,Author,PublicationDate, Enable")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("~/Views/Admin/Articles.cshtml", db.Articles.ToList());
            }
            return PartialView();
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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
