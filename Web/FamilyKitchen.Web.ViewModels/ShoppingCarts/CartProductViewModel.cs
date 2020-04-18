namespace FamilyKitchen.Web.ViewModels.ShoppingCarts
{
    using FamilyKitchen.Web.ViewModels.ShopProducts;

    public class CartProductViewModel
    {
        public decimal Quantity { get; set; }

        public ShopProductViewModel Details { get; set; }
    }
}
