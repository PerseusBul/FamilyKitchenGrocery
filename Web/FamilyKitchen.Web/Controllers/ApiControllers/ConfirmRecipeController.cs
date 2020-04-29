namespace FamilyKitchen.Web.ApiControllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ConfirmRecipeController : ControllerBase
    {
        private readonly IRecipesService recipesService;
        private readonly IShopProductsService shopProductsService;
        private readonly IFavoriteProductService favoriteProductService;

        public ConfirmRecipeController(IRecipesService recipesService, IShopProductsService shopProductsService, IFavoriteProductService favoriteProductService)
        {
            this.recipesService = recipesService;
            this.shopProductsService = shopProductsService;
            this.favoriteProductService = favoriteProductService;
        }

        [HttpPost]
        public async Task<ActionResult<ConfirmRecipeResponseModel>> ProducePrivateMeal(PrivateMealInputModel input)
        {
            var username = this.User.Identity.Name;

            var recipe = this.recipesService.GetRecipeByRecipeId(input.RecipeId);

            if (username == null || recipe == null)
            {
                return this.RedirectToAction("AllMeals", "ShopProducts");
            }

            var product = this.shopProductsService.ProduceNewKitchenProducts(recipe).First();

            var actionAddProduct = await this.shopProductsService.Add(product);

            if (!actionAddProduct)
            {
                return this.RedirectToAction("AllMeals", "ShopProducts");
            }

            var actionAddFavorite = await this.favoriteProductService.Add(product.Id, username);

            if (!actionAddFavorite)
            {
                return this.RedirectToAction("AllMeals", "ShopProducts");
            }

            var result = new ConfirmRecipeResponseModel
            {
                ProductId = product.Id,
            };

            return result;
        }
    }
}
