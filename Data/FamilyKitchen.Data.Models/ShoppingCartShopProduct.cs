namespace FamilyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCartShopProduct
    {
        [Required]
        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        [Required]
        public int ShopProductId { get; set; }

        public ShopProduct ShopProduct { get; set; }

        [Required]
        public decimal Quantity { get; set; }
    }
}
