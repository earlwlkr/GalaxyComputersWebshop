using GalaxyComputersASP.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GalaxyComputersASP.Controllers
{
    public class ShoppingController : Controller
    {
        private GalaxyComputersASPContext db = new GalaxyComputersASPContext();

        private const string CART_SESSION_KEY = "CartID";

        private string getCartId(HttpContextBase context)
        {
            if (context.Session[CART_SESSION_KEY] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CART_SESSION_KEY] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CART_SESSION_KEY] = tempCartId.ToString();
                }
            }
            return context.Session[CART_SESSION_KEY].ToString();
        }

        [HttpPost]
        public ActionResult AddToCart()
        {
            int productToAdd = int.Parse(Request.Form["ProductID"]);
            Product product = db.Products.Find(productToAdd);
            if (product == null)
            {
                return Json(new { success = false });
            }
            int quantity = int.Parse(Request.Form["Quantity"]);
            if (quantity < 1)
            {
                quantity = 1;
            }
            var cartId = getCartId(this.HttpContext);
            var cartItem = db.CartItems.SingleOrDefault(
                c => c.UserID == cartId
                && c.ProductID == productToAdd
            );

            if (cartItem == null)
            {
                cartItem = new CartItem {
                    ProductID = productToAdd,
                    UserID = cartId,
                    Quantity = quantity
                };
                db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            db.SaveChanges();
            return Json(new { success = true, product_name = product.Name });
        }

        public ActionResult GetCartItemsCount()
        {
            var cartId = getCartId(this.HttpContext);
            IQueryable<CartItem> cartItems = db.CartItems.Where(i => i.UserID == cartId);
            int count = 0;
            if (cartItems.Count() > 0)
            {
                count = cartItems.Sum(i => i.Quantity);
            }
            return Json(new { success = true, count = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCartItemsPrice()
        {
            var cartId = getCartId(this.HttpContext);
            IQueryable<CartItem> cartItems = db.CartItems.Where(i => i.UserID == cartId);
            double price = 0;
            if (cartItems.Count() > 0)
            {
                price = cartItems.Sum(i => i.Quantity * i.Product.Price);
            }
            return Json(new { success = true, price = price }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCartItems()
        {
            var cartId = getCartId(this.HttpContext);
            List<CartItem> cartItems = db.CartItems.Where(i => i.UserID == cartId).ToList();
            dynamic data = new ExpandoObject();
            if (cartItems.Count() > 0)
            {
                data.items = new List<object>();
                foreach (var item in cartItems)
                {
                    data.items.Add(new { 
                        name = item.Product.Name, 
                        price = item.Product.Price, 
                        quantity = item.Quantity 
                    });
                }
                data.success = true;
            }
            data = ((ExpandoObject)data).ToDictionary(x => x.Key, x => x.Value);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Shopping
        public ActionResult Index()
        {
            return View();
        }
    }
}