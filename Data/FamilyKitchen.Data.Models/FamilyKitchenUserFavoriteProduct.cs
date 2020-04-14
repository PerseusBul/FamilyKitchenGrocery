namespace FamilyKitchen.Data.Models
{
    public class FamilyKitchenUserFavoriteProduct
    {
        public string FamilyKitchenUserId { get; set; }

        public FamilyKitchenUser FamilyKitchenUser { get; set; }

        public int ShopProductId { get; set; }

        public ShopProduct ShopProduct { get; set; }
    }
}
