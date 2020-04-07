namespace FamilyKitchen.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class FoodResource : BaseProduct
    {
        public FoodResource()
        {
            this.IsDeleted = false;
            this.CreatedOn = DateTime.UtcNow;
            this.ExpireDate = this.CreatedOn.AddMonths(4); // TODO add period from delivery
            this.FoodResourcesAllergens = new HashSet<FoodResourceAllergen>();
            this.FoodResourcesRecipes = new HashSet<FoodResourceRecipe>();
        }

        public int? NutritionDeclarationId { get; set; }

        public virtual NutritionDeclaration NutritionDeclaration { get; set; }

        public virtual IEnumerable<FoodResourceAllergen> FoodResourcesAllergens { get; set; }

        public virtual IEnumerable<FoodResourceRecipe> FoodResourcesRecipes { get; set; }
    }
}
