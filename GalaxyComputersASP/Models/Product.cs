using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GalaxyComputersASP.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public string Brand { get; set; }
        public string ImagePath { get; set; }
        public int Views { get; set; }
        public int Sales { get; set; }

        public virtual Category Category { get; set; }
    }
}