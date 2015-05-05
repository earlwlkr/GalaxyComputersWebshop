using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalaxyComputersASP.Models
{
    public class Manufacturer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebPage { get; set; }
        public string ImagePath { get; set; }
        public string Country { get; set; }
        public DateTime DateFounded { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}