using KTTH_BE.Models;
using KTTH_BE.Models.ViewModel;
using PagedList;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace KTTH_BE.Controllers
{
    public class HomeController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        public ActionResult Index(string searchTerm, int? page)
        {
            var model = new HomeProductVM();
            var products = db.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;
                products = products.Where(p => p.ProductName.Contains(searchTerm) || p.ProductDescription.Contains(searchTerm) || p.Category.CategoryName.Contains(searchTerm));
            }
            int pageNumber = page ?? 1;
            model.FeaturedProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(10).ToList();
            model.NewProducts = products.OrderBy(p => p.OrderDetails.Count()).Take(20).ToPagedList(pageNumber, 6);
            return View(model);
        }

        public ActionResult ProductDetails(int? id, int? quantity)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Product pro = db.Products.Find(id);
            if (pro == null) return HttpNotFound();

            var model = new ProductDetailsVM
            {
                product = pro,
                quantity = quantity ?? 1,
                estimatedValue = (quantity ?? 1) * pro.ProductPrice,
                RelatedProducts = db.Products.Where(p => p.CategoryID == pro.CategoryID && p.ProductID != pro.ProductID).Take(8).ToList()
            };
            return View(model);
        }
    }
}