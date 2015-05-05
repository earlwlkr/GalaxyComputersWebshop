﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalaxyComputersASP.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string ImagePath { get; set; }
        public int Views { get; set; }
        public int Sales { get; set; }
    }
}