namespace FamilyKitchen.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyKitchen.Data.Common.Models;
    using FamilyKitchen.Data.Models.Enums;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.OrdersShopProducts = new HashSet<OrderShopProduct>();
            this.DeliveryPrice = 6.99m;
        }

        [Required]
        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime? OrderDate { get; set; }

        public string InvoiceNumber => $"{this.Id}".PadLeft(10, '0');

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
