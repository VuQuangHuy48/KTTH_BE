using KTTH_BE.Models;
using KTTH_BE.Models.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace KTTH_BE.Controllers
{
    public class OrderController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        [Authorize]
        public ActionResult Checkout()
        {
            var cart = (Session["Cart"] as Cart)?.Items.ToList();
            if (cart == null || !cart.Any()) return RedirectToAction("Index", "Home");

            var customer = db.Customers.SingleOrDefault(c => c.Username == User.Identity.Name);
            if (customer == null) return RedirectToAction("Login", "Account");

            var model = new CheckoutVM
            {
                CartItems = cart,
                TotalAmount = cart.Sum(i => i.TotalPrice),
                OrderDate = DateTime.Now,
                ShippingAddress = customer.CustomerAddress,
                CustomerID = customer.CustomerID,
                Username = customer.Username
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Checkout(CheckoutVM model)
        {
            var cart = (Session["Cart"] as Cart)?.Items.ToList();
            if (cart == null || !cart.Any()) return RedirectToAction("Index", "Home");

            var customer = db.Customers.SingleOrDefault(c => c.Username == User.Identity.Name);
            var order = new Order
            {
                CustomerID = customer.CustomerID,
                OrderDate = DateTime.Now,
                TotalAmount = cart.Sum(i => i.TotalPrice),
                PaymentStatus = "Chưa thanh toán",
                AddressDelivery = model.ShippingAddress,
                OrderDetails = cart.Select(i => new OrderDetail
                {
                    ProductID = i.ProductID,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
            db.Orders.Add(order);
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("OrderSuccess", new { id = order.OrderID });
        }

        public ActionResult OrderSuccess(int id)
        {
            var order = db.Orders.Include("OrderDetails").SingleOrDefault(o => o.OrderID == id);
            if (order == null) return HttpNotFound();
            return View(order);
        }
    }
}