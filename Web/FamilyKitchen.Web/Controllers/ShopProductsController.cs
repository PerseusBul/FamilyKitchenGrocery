using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyKitchen.Services.Data;
using FamilyKitchen.Web.ViewModels.FoodResources;
using FamilyKitchen.Web.ViewModels.ShopProducts;
using Microsoft.AspNetCore.Mvc;

namespace FamilyKitchen.Web.Controllers
{
    public class ShopProductsController : BaseController
    {
        private readonly IShopProductsService productsService;

        public ShopProductsController(IShopProductsService productsService) // TODO to be changed with ShopProducts
        {
            this.productsService = productsService;
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.productsService.GetProductById<ProductDetailsViewModel>(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> AllProducts()
        {
            var viewModel = new ListAllProductsViewModel
            {
                Products = this.productsService.GetAllProducts<ShopProductViewModel>(), // await
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> AllMeals()
        {
            var viewModel = new ListAllMealsViewModel
            {
                Products = this.productsService.GetAllMeals<MealViewModel>(), // await
            };

            return this.View(viewModel);
        }
    }
}