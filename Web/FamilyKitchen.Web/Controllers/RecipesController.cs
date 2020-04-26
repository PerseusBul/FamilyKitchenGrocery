namespace FamilyKitchen.Web.Controllers
{
    using System.Linq;

    using FamilyKitchen.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : BaseController
    {
        private readonly IRecipesService recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        [Authorize]
        public IActionResult CustomRecipe(int id)
        {
            var recipe = this.recipesService.GetRecipeByProductId(id);

            if (recipe.FirstOrDefault() == null)
            {
                return this.RedirectToAction("AllMeals", "ShopProducts");
            }

            var viewModel = this.recipesService.CollectTotalRecipeInfo(recipe).GetAwaiter().GetResult();

            if (viewModel == null)
            {
                return this.RedirectToAction("AllMeals", "ShopProducts");
            }

            return this.View(viewModel);
        }

        public IActionResult Order(int id)
        {
            var recipe = this.recipesService.GetRecipeByRecipeId(id);

            if (recipe == null)
            {
                return this.RedirectToAction("AllMeals", "ShopProducts");
            }

            var viewModel = this.recipesService.CollectTotalRecipeInfo(recipe).GetAwaiter().GetResult();

            if (viewModel == null)
            {
                return this.RedirectToAction("AllMeals", "ShopProducts");
            }

            return this.View(viewModel);
        }
    }
}
