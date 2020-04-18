using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyKitchen.Data.Models
{
    public class OrderShopProduct
    {
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int ShopProductId { get; set; }

        public virtual ShopProduct ShopProduct { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
