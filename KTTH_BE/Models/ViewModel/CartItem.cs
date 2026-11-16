using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTTH_BE.Models.ViewModel
{
    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductImage { get; set; }
        public string Category { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }

    public class Cart
    {
        private List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items => items;

        public void AddItem(int productId, string productImage, string productName, decimal unitPrice, int quantity, string category)
        {
            var existing = items.FirstOrDefault(i => i.ProductID == productId);
            if (existing == null)
                items.Add(new CartItem { ProductID = productId, ProductImage = productImage, ProductName = productName, UnitPrice = unitPrice, Quantity = quantity, Category = category });
            else
                existing.Quantity += quantity;
        }
        public void RemoveItem(int productId) => items.RemoveAll(i => i.ProductID == productId);
        public decimal TotalValue() => items.Sum(i => i.TotalPrice);
        public void Clear() => items.Clear();
        public void UpdateQuantity(int productId, int quantity)
        {
            var item = items.FirstOrDefault(i => i.ProductID == productId);
            if (item != null) item.Quantity = quantity;
        }
    }

    public class CartService
    {
        private readonly HttpSessionStateBase session;
        public CartService(HttpSessionStateBase session) { this.session = session; }
        public Cart GetCart()
        {
            var cart = (Cart)session["Cart"];
            if (cart == null) { cart = new Cart(); session["Cart"] = cart; }
            return cart;
        }
        public void ClearCart() => session["Cart"] = null;
    }
}