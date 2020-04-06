namespace FamilyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ShopProductSubCategory
    {
        [Required]
        public int ShopProductId { get; set; }

        public ShopProduct ShopProduct { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }
    }
}
