using GalaxyComputersASP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GalaxyComputersASP.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private GalaxyComputersASPContext db = new GalaxyComputersASPContext();

        // GET: /Admin
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
            List<ApplicationUser> users = userManager.Users.ToList();

            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new GalaxyComputersASPContext()));
            List<IdentityRole> allRoles = roleManager.Roles.ToList();
            var rolesList = new List<SelectListItem>();
            foreach (IdentityRole role in allRoles)
            {
                string roleName = role.Name;
                rolesList.Add(new SelectListItem { Text = roleName, Value = roleName });
            }
            SelectList roles = new SelectList(rolesList);
            //var str = roleManager.Create(new IdentityRole(roleName));

            List<String> userRoles = new List<string>();
            foreach (ApplicationUser user in users)
            {
                foreach (SelectListItem roleItem in rolesList)
                {
                    string role = roleItem.Text;
                    if (userManager.IsInRole(user.Id, role))
                    {
                        userRoles.Add(role);
                        break;
                    }
                }
            }

            IEnumerable<Product> products = db.Products.ToList();
            List<ProductOverview> list = new List<ProductOverview>();
            
            foreach (Product product in products)
            {
                Category category = db.Categories.Find(product.CategoryID);
                Manufacturer manufacturer = db.Manufacturers.Find(product.ManufacturerID);
                list.Add(new ProductOverview { ProductData = product, ProductCategory = category, ProductManufacturer = manufacturer });
            }
            return View(new AdminViewModel
            {
                Users = users.ToList(),
                UserRoles = userRoles,
                Roles = roles,
                Products = list.ToList(),
                Categories = db.Categories.ToList(),
                Manufacturers = db.Manufacturers.ToList()
            });
        }
    }
}