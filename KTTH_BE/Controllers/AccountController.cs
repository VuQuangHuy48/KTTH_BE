using KTTH_BE.Models;
using KTTH_BE.Models.ViewModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace KTTH_BE.Controllers
{
    public class AccountController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        public ActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại!");
                    return View(model);
                }
                db.Users.Add(new User { Username = model.Username, Password = model.Password, UserRole = "Customer" });
                db.Customers.Add(new Customer { CustomerName = model.CustomerName, CustomerEmail = model.CustomerEmail, CustomerPhone = model.CustomerPhone, CustomerAddress = model.CustomerAddress, Username = model.Username });
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public ActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password && u.UserRole == "Customer");
                if (user != null)
                {
                    Session["Username"] = user.Username;
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu.");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}