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
    public class PartialIndexViewModel
    {
        public IEnumerable<Product> Products;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public Nullable<int> CategoryID { get; set; }
    }
   
    public class ProductDetailsViewModel
    {
        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}
