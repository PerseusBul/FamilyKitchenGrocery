namespace FamilyKitchen.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.ShoppingCarts;
    using FamilyKitchen.Web.ViewModels.ShopProducts;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartsController : BaseController
    {
        private readonly IShoppingCartsService cartsService;
        private readonly IDeletableEntityRepository<FamilyKitchenUser> userRepository;

        public ShoppingCartsController(IShoppingCartsService cartsService,
                                       IDeletableEntityRepository<FamilyKitchenUser> userRepository)
        {
            this.cartsService = cartsService;
            this.userRepository = userRepository;
        }

        public IActionResult GetCart()
        {
            var viewModel = new ListAllCartProductsViewModel()
            {
                CartProducts = this.cartsService.GetAllProducts(this.User.Identity.Name).ToList(),
            };

            if (viewModel.CartProducts == null)
            {
                this.Redirect("/");
            }

            return this.View(viewModel);
        }

        public IActionResult DeleteCartProduct(int id)
        {

            var action = this.cartsService.DeleteProduct(id, this.User.Identity.Name);

            if (!action.Result)
            {
                this.Redirect("/");
            }

            return this.RedirectToAction(nameof(GetCart));
        }

        public IActionResult Add(int id)
        {

            var action = this.cartsService.AddProduct(id, this.User.Identity.Name);

            if (!action.Result)
            {
                this.Redirect("/");
            }

            return this.RedirectToAction(nameof(GetCart));
        }

        public IActionResult DeleteCart(int id)
        {

            var action = this.cartsService.DeleteAll(this.User.Identity.Name);

            if (!action)
            {
                this.RedirectToAction(nameof(GetCart));
            }

            return this.Redirect("/");
        }

        public IActionResult GetCartInitial()
        {
            var product = new ShopProductViewModel()
            {
                Id = 2,
                Name = "Bear",
                Price = 2,
                Discount = 10,
            };

            var user = userRepository.All().Where(x => x.Id == "d3d03427-f9fa-43c2-b88b-5695decf3326").FirstOrDefault();

            var model = new List<ShopProductViewModel>() { product };
            ExtensionsConf.SessionConf.SessionExtensions.SetDataObject<List<ShopProductViewModel>>(this.HttpContext.Session, "product", model);
            var backProduct = ExtensionsConf.SessionConf.SessionExtensions.GetDataObject<List<ShopProductViewModel>>(this.HttpContext.Session, "product");
            return this.View();
        }

    }
}
