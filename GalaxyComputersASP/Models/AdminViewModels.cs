using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalaxyComputersASP.Models
{
    public class AdminViewModel
    {
        public IEnumerable<ApplicationUser> Users;
        public IEnumerable<ProductOverview> Products;
        public IEnumerable<Category> Categories;
        public IEnumerable<Manufacturer> Manufacturers;
    }
}