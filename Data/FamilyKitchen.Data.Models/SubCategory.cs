namespace FamilyKitchen.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyKitchen.Data.Common.Models;

    public class SubCategory : BaseDeletableModel<int>
    {
        public SubCategory()
        {
            this.IsDeleted = false;
            this.CreatedOn = DateTime.UtcNow;
            this.ShopProductsSubCategories = new HashSet<ShopProductSubCategory>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public virtual IEnumerable<ShopProductSubCategory> ShopProductsSubCategories { get; set; }
    }
}
