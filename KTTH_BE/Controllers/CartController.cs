using KTTH_BE.Models;
using KTTH_BE.Models.ViewModel;
using System.Web.Mvc;

namespace KTTH_BE.Controllers
{
    public class CartController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        private CartService GetCartService() => new CartService(Session);

        public ActionResult Index() => View(GetCartService().GetCart());

        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var product = db.Products.Find(id);
            if (product != null)
                GetCartService().GetCart().AddItem(product.ProductID, product.ProductImage, product.ProductName, product.ProductPrice, quantity, product.Category.CategoryName);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            GetCartService().GetCart().RemoveItem(id);
            return RedirectToAction("Index");
        }

        public ActionResult ClearCart()
        {
            GetCartService().ClearCart();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            if (quantity > 0) GetCartService().GetCart().UpdateQuantity(id, quantity);
            return RedirectToAction("Index");
        }
    }
}