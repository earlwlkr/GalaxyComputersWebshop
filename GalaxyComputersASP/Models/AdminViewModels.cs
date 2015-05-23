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
        public SelectList Roles;
        public IEnumerable<ProductOverview> Products;
        public IEnumerable<Category> Categories;
        public IEnumerable<Manufacturer> Manufacturers;
    }
}