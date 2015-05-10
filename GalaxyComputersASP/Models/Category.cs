using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalaxyComputersASP.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}