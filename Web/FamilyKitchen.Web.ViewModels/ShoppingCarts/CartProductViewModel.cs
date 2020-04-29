namespace FamilyKitchen.Web.ViewModels.ShoppingCarts
{
    using System;
    using FamilyKitchen.Common;
    using FamilyKitchen.Web.ViewModels.ShopProducts;

    public class CartProductViewModel
    {
        public decimal Quantity { get; set; }

        public ShopProductViewModel Details { get; set; }

        public decimal CartProductTotal => Math.Round(this.Quantity * this.Details.SalePrice, GlobalConstants.FractionalDigits);
    }
}
