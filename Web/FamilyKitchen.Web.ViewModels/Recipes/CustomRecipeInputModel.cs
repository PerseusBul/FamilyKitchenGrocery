namespace FamilyKitchen.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class CustomRecipeInputModel
    {
        public int RecipeId { get; set; }

        public IEnumerable<RecipeResourceInputModel> Resources { get; set; }
    }
}
