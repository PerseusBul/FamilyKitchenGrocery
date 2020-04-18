using FamilyKitchen.Data.Common.Models;
using FamilyKitchen.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FamilyKitchen.Data.Models
{
    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.OrdersShopProducts = new HashSet<OrderShopProduct>();
        }

        [Required]
        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime? OrderDate { get; set; }

        public string InvoiceNumber { get; set; }

        public string EasyPayNumber { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal DeliveryPrice { get; set; }

        [Required]
        public string FamilyKitchenUserId { get; set; }

        public FamilyKitchenUser FamilyKitchenUser { get; set; }

        public string OrderProfileId { get; set; }

        public virtual OrderProfile OrderProfile { get; set; }

        public virtual IEnumerable<OrderShopProduct> OrdersShopProducts { get; set; }
    }
}
