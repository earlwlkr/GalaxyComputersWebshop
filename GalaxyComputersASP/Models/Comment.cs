using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GalaxyComputersASP.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        [Display(Name = "Thời gian gửi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime PublishDate { get; set; }
        [ForeignKey("User")]
        [Display(Name = "Người gửi")]
        public string UserID { get; set; }
        [Display(Name = "Lượt thích")]
        public int Likes { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}