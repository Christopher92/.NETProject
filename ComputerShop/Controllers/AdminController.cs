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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace ComputerShop.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ComputerShopContext db = new ComputerShopContext();

        // GET: Admin
        [Authorize(Roles = "Administrator, Sprzedawca")]
        public ActionResult Index()
        {
            var today = DateTime.Now.Date;

            double dailySum = 0;
            int ordersSum = 0;
            int productsSum = 0;
            int realised = 0;
            var orders = db.Orders.Include(o => o.OrderPayment).Include(o => o.OrderState).Include(o => o.OrderSupply).Include(o => o.User).Where(x => x.OrderStateID != 1);

            if (orders.Any())
            {
                foreach (var x in orders.Where(x => DateTime.Compare(x.IncomingDate ?? DateTime.MinValue, today) > 0))
                {
                    dailySum += x.Suma;
                    ordersSum++;
                    if (x.OrderStateID == 3)
                        realised++;
                }
                Raport raport = db.Raports.Where(x => (DateTime.Compare(x.Date, today) == 0)).FirstOrDefault();

                var order = orders.Where(x => (DateTime.Compare(x.IncomingDate ?? DateTime.MinValue, today) > 0)).FirstOrDefault();
                if (order != null)
                {
                    int orderId = order.OrderID;
                    foreach (var x in db.OrderProducts.Where(x => x.OrderID == orderId))
                    {
                        productsSum += x.Quantity;
                    }


                    if (raport == null)
                    {
                        Raport raportX = new Raport { Date = today, DailyRevenue = dailySum, ProductsQuantity = productsSum, OrdersQuantity = ordersSum, Realised = realised };
                        db.Raports.Add(raportX);
                        db.SaveChanges();
                    }
                    else
                    {
                        raport.DailyRevenue = dailySum;
                        raport.ProductsQuantity = productsSum;
                        raport.OrdersQuantity = ordersSum;
                        raport.Realised = realised;

                        db.Entry(raport).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return View(db.Raports);
                }
                else
                {
                    if (raport == null)
                    {
                        Raport raportX = new Raport { Date = today, DailyRevenue = dailySum, ProductsQuantity = productsSum, OrdersQuantity = ordersSum, Realised = realised };
                        db.Raports.Add(raportX);
                        db.SaveChanges();
                    }
                }

            }

            return View(db.Raports);
        }
        [Authorize(Roles = "Administrator, Sprzedawca")]

        public PartialViewResult IndexForAjax()
        {
            var orders = db.Orders.Include(o => o.OrderPayment).Include(o => o.OrderState).Include(o => o.OrderSupply).Include(o => o.User).Where(x => x.OrderStateID != 1);
            return PartialView("Order", orders.ToList());
        }
        [Authorize(Roles = "Administrator, Sprzedawca")]
        [HttpPost]
        public PartialViewResult GetOrderProducts(int id)
        {
            var products = db.OrderProducts.Include(o => o.Product).Where(x => x.OrderID == id);
            return PartialView("OrderProducts", products.ToList());
        }

        //[HttpPost]
        //public PartialViewResult Create()
        //{
        //    var products = db.OrderProducts.Include(o => o.Product).Where(x => x.OrderID == id);
        //    return PartialView("OrderProducts", products.ToList());
        //}

        [Authorize(Roles = "Administrator, Sprzedawca")]
        public PartialViewResult GetProducts()
        {
            var products = db.Products;
            return PartialView("Products", products.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Sprzedawca")]
        public PartialViewResult GetUsers()
        {
            var users = UserManager.Users.Include(x => x.Roles);

            return PartialView("Users", users.ToList());
        }

        [Authorize(Roles = "Administrator, Sprzedawca")]
        public PartialViewResult GetCategories()
        {
            var categories = db.Categories;
            return PartialView("Categories", categories.ToList());
        }
        [Authorize(Roles = "Administrator")]
        [Authorize(Roles = "Sprzedawca")]
        public PartialViewResult GetArticles()
        {
            var articles = db.Articles;
            return PartialView("Articles", articles.ToList());
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public PartialViewResult GetRegulations()
        {
            var regulations = db.Regulations;
            return PartialView("Regulations", regulations.ToList());
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public PartialViewResult GetOrderSupplies()
        {
            var orderSupply = db.OrderSupplies;
            return PartialView("OrderSupplies", orderSupply.ToList());
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public PartialViewResult GetOrderPayments()
        {
            var orderPayment = db.OrderPayments;
            return PartialView("OrderPayments", orderPayment.ToList());
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public PartialViewResult GetContact()
        {
            Contact contact = db.Contacts.FirstOrDefault();
            return PartialView("Contact", contact);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
            }
            return PartialView("Contact", contact);
        }
        //private async Task<string> SendEmailConfirmationTokenAsync(string userID, string subject)
        [Authorize(Roles = "Sprzedawca")]
        public async Task Realize(int id)
        {
            //TODO: send email to user
            var order = db.Orders.Find(id);
            order.OrderStateID = 3;
            order.OutcomingDate = DateTime.Now;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            string content = "Szanowny użytkowniku " + order.User.Name + " " + order.User.Surname + "<br>O godzinie " + order.OutcomingDate + " zostało zrealizowane Twoje zamówienie.";
            await UserManager.SendEmailAsync(order.UserID, "Realizacja zamówienia", content);
        }
        [Authorize(Roles = "Sprzedawca")]
        public async Task Reject(int id)
        {
            //TODO: send email to user
            var order = db.Orders.Find(id);
            order.OrderStateID = 4;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            string content = "Szanowny użytkowniku " + order.User.Name + " " + order.User.Surname + "<br>O godzinie " + order.OutcomingDate + " zostało odrzucone Twoje zamówienie.";
            await UserManager.SendEmailAsync(order.UserID, "Realizacja zamówienia", content);
        }
    }
}