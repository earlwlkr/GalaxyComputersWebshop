using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GalaxyComputersASP.Models
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập Email!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Dữ liệu Họ không hợp lệ!")]
        [Display(Name = "Họ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Dữ liệu Tên không hợp lệ!")]
        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ!")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại!")]
        [RegularExpression(@"09\d{8}|01\d{9}", ErrorMessage = "Số điện thoại không hợp lệ!")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
    }

    public class OrdersViewModel
    {
        public List<Order> Orders { get; set; }
        public List<double> Prices { get; set; }
        public List<List<OrderItem>> OrderItems { get; set; }
    }
}