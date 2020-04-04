namespace FamilyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FamilyKitchen.Data.Common.Models;

    public class SubCategory : BaseDeletableModel<int>
    {
        //public SubCategory()
        //{
        //    this.ShopProductsSubCategories = new HashSet<ShopProductSubCategory>();
        //}

        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

       // public virtual ICollection<ShopProductSubCategory> ShopProductsSubCategories { get; set; }
    }
}
