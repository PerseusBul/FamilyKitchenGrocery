namespace FamilyKitchen.Web.ApiControllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class CustomRecipesController : ControllerBase
    {
        private readonly IRecipesService recipesService;

        public CustomRecipesController(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        [HttpPost]
        public async Task<ActionResult<CustomRecipeResponseModel>> Create(CustomRecipeInputModel input)
        {
            var baseRecipe = this.recipesService.GetRecipeByRecipeId(input.RecipeId);

            var customRecipe = await this.recipesService.CreatePrivateRecipe(baseRecipe, input.Resources, this.User.Identity.Name);

            if (customRecipe == null)
            {
                return this.RedirectToAction("AllMeals", "ShopProducts");
            }

            var response = new CustomRecipeResponseModel
            {
                RecipeId = customRecipe.FirstOrDefault().Id,
            };

            return response;
        }
    }
}
