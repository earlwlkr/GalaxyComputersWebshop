using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalaxyComputersASP.Models
{
    public class ProductOverview
    {
        public Category ProductCategory { get; set; }
        public Manufacturer ProductManufacturer { get; set; }
        public Product ProductData { get; set; }
    }
    public class ProductManageViewModel
    {
        public IEnumerable<ProductOverview> Products;
        public IEnumerable<Category> Categories;
        public IEnumerable<Manufacturer> Manufacturers;
    }
    public class ProductDetailsViewModel
    {
        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}
