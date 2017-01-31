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
using System.Web.UI;
namespace ComputerShop.Controllers
{
    public class OrderProductsController : Controller
    {
        private ComputerShopContext db = new ComputerShopContext();


        [Authorize]
        public void AddOneProductToCart(int? id, int quantity = 1)
        {
            var userId = User.Identity.GetUserId();
            if (id == null)
            {

            }
            Product product = db.Products.Find(id);
            if (product.Quantity < quantity)
                quantity = product.Quantity;

            OrderProduct orderProduct = db.OrderProducts.Where(y => y.ProductOrderStateID == 1).FirstOrDefault(x => x.Product.ProductID == id);
            if (orderProduct == null)
            {
                Order orderX = db.Orders.Where(y => y.OrderStateID == 1).FirstOrDefault(x => x.UserID == userId);
                if (orderX == null)
                {
                    Order order = new Order { UserID = userId, OrderStateID = 1, OrderPaymentID = 1, OrderSupplyID = 1};
                    db.Orders.Add(order);
                    db.SaveChanges();
                }

                Order orderY = db.Orders.Where(y => y.OrderStateID == 1).FirstOrDefault(x => x.UserID == userId);
                int orderId = orderY.OrderID;



                OrderProduct OP = new OrderProduct { Product = product, Quantity = quantity, ProductOrderStateID = 1, OrderID = orderId, userID = userId };
                OP.Suma = OP.Product.Price * OP.Quantity;
                db.OrderProducts.Add(OP);
                db.SaveChanges();
            }


            else
            {
                orderProduct.Quantity += quantity;
                orderProduct.Suma = orderProduct.Product.Price * orderProduct.Quantity;
                db.Entry(orderProduct).State = EntityState.Modified;
                db.SaveChanges();
            }
            product.Quantity -= quantity;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
        }

        [HttpPost]
        public void AddProducts(int id, int quantity)
        {

        }

        public void RemoveOneProductFromCart(int? id)
        {
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct != null)
            {
                if (orderProduct.Quantity > 1)
                {
                    orderProduct.Quantity--;
                    db.Entry(orderProduct).State = EntityState.Modified;
                }
                else
                {
                    db.OrderProducts.Remove(orderProduct);
                }
                var product = db.Products.Find(orderProduct.ProductID);
                product.Quantity++;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void RemoveEntireProductFromCart(int? id)
        {
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct != null)
            {
                db.OrderProducts.Remove(orderProduct);
                //db.Entry(orderProduct).State = EntityState.Modified;
                db.SaveChanges();

                var product = db.Products.Find(orderProduct.ProductID);
                product.Quantity += orderProduct.Quantity;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void RemoveAllProductsFromCart()
        {
            var userId = User.Identity.GetUserId();
            var orderProducts = db.OrderProducts.Where(x => x.Order.UserID == userId).ToList();
            Product product;
            if (orderProducts != null)
            {
                foreach (var item in orderProducts)
                {
                    db.OrderProducts.Remove(item);
                    product = db.Products.Find(item.ProductID);
                    product.Quantity += item.Quantity;
                    db.Entry(product).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Realize(Order order)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                DateTime now = DateTime.Now;
                ICollection<OrderProduct> orderProducts = db.OrderProducts.Where(y => y.ProductOrderStateID == 1).Where(x => x.userID == userId).ToList();
                if (orderProducts.Any())
                {
                    Order orderX = db.Orders.Where(y => y.OrderStateID == 1).FirstOrDefault(x => x.UserID == userId);
                    orderX.Suma = 0;
                    int orderId = orderX.OrderID;
                    foreach (var x in orderProducts)
                    {
                        x.ProductOrderStateID = 2;
                        db.Entry(x).State = EntityState.Modified;
                        orderX.Suma += x.Suma;

                        OrderProductHistory orderProductHistory = new OrderProductHistory { Price = x.Product.Price, OrderProductID = x.OrderProductID };
                        db.OrderProductHistory.Add(orderProductHistory);
                        db.SaveChanges();
                    }
                    OrderUser orderUser = new OrderUser
                    {
                        OrderUserID = order.OrderID,
                        Email = order.OrderUser.Email ?? orderX.User.Email,
                        Name = order.OrderUser.Name ?? orderX.User.Name,
                        Surname = order.OrderUser.Email ?? orderX.User.Surname,
                        Address = order.OrderUser.Surname ?? orderX.User.Address,
                        Telephone = order.OrderUser.Telephone ?? orderX.User.Telephone
                    };
                    db.OrderUser.Add(orderUser);
                    db.SaveChanges();
                    //user.Telephone = order.User.Telephone;
                    //user.Name = order.User.Name;
                    //user.Surname = order.User.Surname;
                    //user.Address = order.User.Address;
                    //user.Agreement = true;
                    //db.Entry(user).State = EntityState.Modified;
                    orderX.OrderUserID = orderX.OrderID;
                    orderX.OrderStateID = 2;
                    orderX.IncomingDate = now;
                    orderX.OrderPaymentID = order.OrderPaymentID;
                    orderX.OrderSupplyID = order.OrderSupplyID;


                    db.Entry(orderX).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            ViewBag.OrderPaymentID = new SelectList(db.OrderPayments, "OrderPaymentID", "Name");
            ViewBag.OrderSupplyID = new SelectList(db.OrderSupplies, "OrderSupplyID", "Name");
            return PartialView("Modal",db.OrderProducts);
        }
        // GET: OrderProducts
        [Authorize]
        [HttpPost]
        public PartialViewResult Index()
        {
            ApplicationDbContext AC = new ApplicationDbContext();

            string userId = User.Identity.GetUserId();
            var user = AC.Users.Find(userId);

            var orderProducts = db.OrderProducts.Include(o => o.Product).Where(x => x.ProductOrderStateID == 1).Where(y => y.Order.UserID == userId);
            ViewBag.OrderPaymentID = new SelectList(db.OrderPayments, "OrderPaymentID", "Name");
            ViewBag.OrderSupplyID = new SelectList(db.OrderSupplies, "OrderSupplyID", "Name");

            var tuple = new Tuple<List<OrderProduct>, ApplicationUser>(orderProducts.ToList(),user);
            return PartialView(tuple);
        }

        public ViewResult IndexForAjax()
        {
            ApplicationDbContext AC = new ApplicationDbContext();

            string userId = User.Identity.GetUserId();
            var user = AC.Users.Find(userId);

            var orderProducts = db.OrderProducts.Include(o => o.Product).Where(x => x.ProductOrderStateID == 1).Where(y => y.Order.UserID == userId);
            ViewBag.OrderPaymentID = new SelectList(db.OrderPayments, "OrderPaymentID", "Name");
            ViewBag.OrderSupplyID = new SelectList(db.OrderSupplies, "OrderSupplyID", "Name");

            var tuple = new Tuple<List<OrderProduct>, ApplicationUser>(orderProducts.ToList(),user);
            return View("Index",tuple);
        }

        // GET: OrderProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return HttpNotFound();
            }
            return View(orderProduct);
        }

        // GET: OrderProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: OrderProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderProductID,ProductID")] OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                orderProduct.Suma = orderProduct.Quantity * orderProduct.Product.Price;
                db.OrderProducts.Add(orderProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderProduct.ProductID);
            return View(orderProduct);
        }

        // GET: OrderProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderProduct.ProductID);
            return View(orderProduct);
        }

        // POST: OrderProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderProductID,ProductID")] OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderProduct.ProductID);
            return View(orderProduct);
        }

        // GET: OrderProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return HttpNotFound();
            }
            return View(orderProduct);
        }

        // POST: OrderProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            db.OrderProducts.Remove(orderProduct);
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
