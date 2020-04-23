namespace FamilyKitchen.Web.Controllers
{
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.ShopProducts;
    using Microsoft.AspNetCore.Mvc;

    public class ShopProductsController : BaseController
    {
        private readonly IShopProductsService productsService;

        public ShopProductsController(IShopProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.productsService.GetProductById<ProductDetailsViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult AllProducts()
        {
            var viewModel = new ListAllProductsViewModel
            {
                Products = this.productsService.GetAllProducts<ShopProductViewModel>(), // TODO await
            };

            return this.View(viewModel);
        }

        public IActionResult AllMeals()
        {
            var viewModel = new ListAllMealsViewModel
            {
                Products = this.productsService.GetAllMeals<ShopProductViewModel>(), // await
            };

            return this.View(viewModel);
        }
    }
}
