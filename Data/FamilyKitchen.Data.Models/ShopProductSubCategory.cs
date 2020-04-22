namespace FamilyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ShopProductSubCategory
    {
        [Required]
        public int ShopProductId { get; set; }

        public virtual ShopProduct ShopProduct { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
}
