namespace FamilyKitchen.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Mapping;
    using FamilyKitchen.Web.ViewModels.FoodResources;

    public class TotalRecipeViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string Size { get; set; }

        public int PreparationTime { get; set; }

        public IEnumerable<string> Allergens { get; set; } = new HashSet<string>();

        public IEnumerable<FoodResourceViewModel> RecipeFoodResources { get; set; } = new HashSet<FoodResourceViewModel>();

        public NutritionDeclaration Nutrients { get; set; }
    }
}
