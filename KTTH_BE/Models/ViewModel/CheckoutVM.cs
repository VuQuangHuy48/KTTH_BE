using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KTTH_BE.Models.ViewModel
{
    public class CheckoutVM
    {
        public List<CartItem> CartItems { get; set; }
        public int CustomerID { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Tổng tiền")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Trạng thái thanh toán")]
        public string PaymentStatus { get; set; }

        [Display(Name = "Phương thức thanh toán")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Phương thức vận chuyển")]
        public string ShippingMethod { get; set; }

        [Display(Name = "Địa chỉ nhận hàng")]
        public string ShippingAddress { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }
    }
}