namespace FamilyKitchen.Data.Models
{
    public class FamilyKitchenUserFavoriteProduct
    {
        public string FamilyKitchenUserId { get; set; }

        public virtual FamilyKitchenUser FamilyKitchenUser { get; set; }

        public int ShopProductId { get; set; }

        public virtual ShopProduct ShopProduct { get; set; }
    }
}
