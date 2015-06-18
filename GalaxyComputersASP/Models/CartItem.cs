using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GalaxyComputersASP.Models
{
    public class CartItem
    {
        public int ID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public string UserID { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
    }
}