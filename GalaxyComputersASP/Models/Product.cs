﻿using System;
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
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Giá tiền")]
        public double Price { get; set; }
        [Display(Name = "Thông tin sản phẩm")]
        public string Description { get; set; }
        [Display(Name = "Ngày ra mắt")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime PublishDate { get; set; }
        [ForeignKey("Category")]
        [Display(Name = "Danh mục")]
        public int CategoryID { get; set; }
        [ForeignKey("Manufacturer")]
        [Display(Name = "Nhà sản xuất")]
        public int ManufacturerID { get; set; }
        [Display(Name = "Hình đại diện")]
        public string ImagePath { get; set; }
        [Display(Name = "Lượt xem")]
        public int Views { get; set; }
        [Display(Name = "Đã bán")]
        public int Sales { get; set; }

        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}