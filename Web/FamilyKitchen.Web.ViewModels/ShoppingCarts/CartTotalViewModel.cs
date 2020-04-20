namespace FamilyKitchen.Web.ViewModels.ShoppingCarts
{
    public class CartTotalViewModel
    {
        public decimal Subtotal { get; set; }

        public decimal DeliveryPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal Total { get; set; }
    }
}
