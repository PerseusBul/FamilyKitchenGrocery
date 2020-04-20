namespace FamilyKitchen.Web.ViewModels.ShoppingCarts
{
    using FamilyKitchen.Web.ViewModels.ShopProducts;
    using System;

    public class CartProductViewModel
    {
        public decimal Quantity { get; set; }

        public ShopProductViewModel Details { get; set; }

        public decimal CartProductTotal => Math.Round(this.Quantity * this.Details.SalePrice, 2);
    }
}
