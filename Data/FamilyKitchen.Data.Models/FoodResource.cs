namespace AspNetCoreTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using AspNetCoreTemplate.Data.Models.Enums;

    public class FoodResource : BaseProduct
    {
        public FoodResource()
        {
            this.IsDeleted = false;
            this.CreatedOn = DateTime.UtcNow;
            this.ExpireDate = this.CreatedOn.AddMonths(4); // TODO add period from delivery
            //this.FoodResourcesAllergens = new HashSet<FoodResourceAllergen>();
            //this.FoodResourcesRecipes = new HashSet<FoodResourceRecipe>();
        }

        public int? NutritionDeclarationId { get; set; }

        public virtual NutritionDeclaration NutritionDeclaration { get; set; }

        //public virtual ICollection<FoodResourceAllergen> FoodResourcesAllergens { get; set; }

        //public virtual ICollection<FoodResourceRecipe> FoodResourcesRecipes { get; set; }
    }
}
