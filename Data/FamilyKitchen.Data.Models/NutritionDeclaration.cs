namespace FamilyKitchen.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FamilyKitchen.Data.Common.Models;

    public class NutritionDeclaration : BaseDeletableModel<int>
    {
        public NutritionDeclaration()
        {
            this.IsDeleted = false;
            this.CreatedOn = DateTime.UtcNow;
        }

        public decimal? Energy { get; set; }

        public decimal? Fats { get; set; }

        public decimal? SaturatedFats { get; set; }

        public decimal? Carbohydrate { get; set; }

        public decimal? Protein { get; set; }

        public decimal? Sodium { get; set; }

        public int GroupIndex { get; set; }

        public virtual ICollection<FoodResource> FoodResources { get; set; }

        //public virtual ICollection<ShopProduct> ShopProducts { get; set; }
    }
}