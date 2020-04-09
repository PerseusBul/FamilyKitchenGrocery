namespace FamilyKitchen.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyKitchen.Data.Common.Models;
    using FamilyKitchen.Data.Models.Enums;

    public class Recipe : BaseDeletableModel<int>
    {
        public Recipe()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
            this.FoodResourcesRecipes = new HashSet<FoodResourceRecipe>();
            //this.FavoritedBy = new HashSet<UserFavoriteRecipe>()
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Summary { get; set; }

        public int PreparationTime { get; set; }

        public Size Size { get; set; }

        public bool? IsPrivate { get; set; }

        // TODO calculate it in the constructor or may be in any service from NutritionDeclaration of the KitchenProducts
        [NotMapped]
        public NutritionDeclaration Nutrients { get; }// => GetNutritionDeclaration(this.Id);

        // TODO calculate it in the constructor or may be in any service from NutritionDeclaration of the KitchenProducts
        public IEnumerable<string> ListAllergens { get; }

        // public IEnumerable<UserFavoriteRecipe> FavoritedBy { get; set; }

        public virtual IEnumerable<FoodResourceRecipe> FoodResourcesRecipes { get; set; }
    }
}
