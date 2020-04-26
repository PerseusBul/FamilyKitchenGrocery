namespace FamilyKitchen.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        NutritionDeclaration GetNutritionDeclaration(int recipeId);

        IQueryable<Recipe> GetRecipeByProductId(int productId);

        IQueryable<Recipe> GetRecipeByRecipeId(int recipeId);

        Task<TotalRecipeViewModel> CollectTotalRecipeInfo(IQueryable<Recipe> recipe);

        Task<IQueryable<Recipe>> CreatePrivateRecipe(IQueryable<Recipe> baseRecipe, IEnumerable<RecipeResourceInputModel> resourses, string username);
    }
}
