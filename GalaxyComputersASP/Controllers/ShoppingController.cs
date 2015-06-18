using GalaxyComputersASP.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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
                string userId = User.Identity.GetUserId();
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    context.Session[CART_SESSION_KEY] = userId;
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

        [HttpPost]
        public ActionResult SetItemQuantity()
        {
            int productToAdd = int.Parse(Request.Form["ProductID"]);
            Product product = db.Products.Find(productToAdd);
            if (product == null)
            {
                return Json(new { success = false });
            }
            int quantity = int.Parse(Request.Form["Quantity"]);
            var cartId = getCartId(this.HttpContext);
            if (quantity <= 0)
            {
                RemoveItemFromCart(productToAdd);
                return Json(new { success = true, product_name = product.Name });
            }
            var cartItem = db.CartItems.SingleOrDefault(
                c => c.UserID == cartId
                && c.ProductID == productToAdd
            );

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductID = productToAdd,
                    UserID = cartId,
                    Quantity = quantity
                };
                db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
            }
            db.SaveChanges();
            return Json(new { success = true, product_name = product.Name });
        }

        [HttpPost]
        public ActionResult RemoveItemFromCart()
        {
            int productToAdd = int.Parse(Request.Form["ProductID"]);
            Product product = db.Products.Find(productToAdd);
            if (product == null)
            {
                return Json(new { success = false });
            }
            RemoveItemFromCart(productToAdd);
            return Json(new { success = true, product_name = product.Name });
        }

        private bool RemoveItemFromCart(int productId)
        {
            var cartId = getCartId(this.HttpContext);
            var cartItem = db.CartItems.SingleOrDefault(
                c => c.UserID == cartId
                && c.ProductID == productId
            );
            db.CartItems.Remove(cartItem);
            db.SaveChanges();
            return true;
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
                        id = item.ProductID,
                        name = item.Product.Name, 
                        price = item.Product.Price, 
                        quantity = item.Quantity 
                    });
                }
            }
            data.success = true;
            data = ((ExpandoObject)data).ToDictionary(x => x.Key, x => x.Value);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Shopping/Checkout
        public ActionResult Checkout()
        {
            return View();
        }

        // POST: Shopping/Checkout
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutViewModel info)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                Order order = new Order
                {
                    UserID = userId,
                    DateCreated = DateTime.Now,
                    Email = info.Email,
                    LastName = info.LastName,
                    FirstName = info.FirstName,
                    PhoneNumber = info.PhoneNumber,
                    Address = info.Address,
                    Status = 0
                };
                order.DateCreated = DateTime.Now;
                db.Orders.Add(order);

                List<CartItem> cartItems = db.CartItems.Where(i => i.UserID == userId).ToList();
                foreach (CartItem item in cartItems)
                {
                    db.OrderItems.Add(new OrderItem
                    {
                        OrderID = order.ID,
                        ProductID = item.ProductID,
                        Quantity = item.Quantity
                    });
                    db.CartItems.Remove(item);
                }

                db.SaveChanges();
                return RedirectToAction("Finish");
            }

            return View(info);
        }

        public ActionResult Orders()
        {
            GalaxyComputersASPContext db = new GalaxyComputersASPContext();
            string userId = User.Identity.GetUserId();
            List<Order> orders = db.Orders.Where(i => i.UserID == userId).ToList();
            List<double> prices = new List<double>();
            List<List<OrderItem>> orderItems = new List<List<OrderItem>>(); 
            foreach (Order order in orders)
            {
                IQueryable<OrderItem> items = db.OrderItems.Where(i => i.OrderID == order.ID);
                prices.Add(items.Sum(i => i.Product.Price * i.Quantity));
                orderItems.Add(items.ToList());
            }
            return View(new OrdersViewModel { Orders = orders, Prices = prices, OrderItems = orderItems });
        }

        public ActionResult Finish()
        {
            return View();
        }
    }
}