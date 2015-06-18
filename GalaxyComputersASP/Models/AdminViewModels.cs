using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GalaxyComputersASP.Models
{
    public class AdminViewModel
    {
        public IEnumerable<ApplicationUser> Users;
        public List<String> UserRoles;
        public SelectList Roles;
        public IEnumerable<ProductOverview> Products;
        public IEnumerable<Category> Categories;
        public IEnumerable<Manufacturer> Manufacturers;
        public List<Order> Orders { get; set; }
        public List<double> Prices { get; set; }
        public List<List<OrderItem>> OrderItems { get; set; }
    }
}