using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalaxyComputersASP.Models
{
    public class ProductOverview
    {
        public Category Category { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Product Product { get; set; }
    }
    public class ProductDetailsViewModel
    {
        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}
