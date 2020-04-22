namespace FamilyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCartShopProduct
    {
        [Required]
        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        [Required]
        public int ShopProductId { get; set; }

        public virtual ShopProduct ShopProduct { get; set; }

        [Required]
        public decimal Quantity { get; set; }
    }
}
