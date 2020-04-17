using FamilyKitchen.Web.ViewModels.ShopProducts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyKitchen.Web.ViewModels.ShoppingCarts
{
    public class CartProductViewModel
    {
        public decimal Quantity { get; set; }

        public ShopProductViewModel Details { get; set; }
    }
}
