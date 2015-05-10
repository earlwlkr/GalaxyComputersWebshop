using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GalaxyComputersASP.Models
{
    public class Manufacturer
    {
        public int ID { get; set; }
        [Display(Name = "Tên nhà sản xuất")]
        public string Name { get; set; }
        [Display(Name = "Mô tả danh mục")]
        public string Description { get; set; }
        [Display(Name = "Trang chủ")]
        public string WebPage { get; set; }
        [Display(Name = "Đường dẫn hình ảnh")]
        public string ImagePath { get; set; }
        [Display(Name = "Quốc gia")]
        public string Country { get; set; }
        [Display(Name = "Ngày thành lập")]
        public DateTime DateFounded { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}