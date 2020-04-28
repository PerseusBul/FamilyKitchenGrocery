namespace FamilyKitchen.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ExtensionsConf.SessionConf;
    using FamilyKitchen.Web.ViewModels.ShoppingCarts;
    using FamilyKitchen.Web.ViewModels.ShopProducts;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartsController : BaseController
    {
        private readonly IShoppingCartsService cartsService;
        private readonly IShopProductsService shopProductsService;
        private readonly IDeletableEntityRepository<FamilyKitchenUser> userRepository;
        private readonly IMapper mapper;

        public ShoppingCartsController(IShoppingCartsService cartsService,
                                        IShopProductsService shopProductsService,
                                        IDeletableEntityRepository<FamilyKitchenUser> userRepository,
                                        IMapper mapper)
        {
            this.cartsService = cartsService;
            this.shopProductsService = shopProductsService;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public IActionResult GetCart()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var viewModel = new ListAllCartProductsViewModel()
                {
                    CartProducts = this.cartsService.GetAllProducts(this.User.Identity.Name).ToList(),
                };

                if (viewModel.CartProducts == null)
                {
                    return this.Redirect("/");
                }

                return this.View(viewModel);
            }

            var sessionCart = SessionExtensions
                .GetDataObject<List<CartProductViewModel>>(this.HttpContext.Session, "shoppingCart");

            if (sessionCart == null || sessionCart.Count() == 0)
            {
                return this.Redirect("/");
            }

            var viewSessionModel = new ListAllCartProductsViewModel()
            {
                CartProducts = sessionCart,
            };

            return this.View(viewSessionModel);
        }

        public IActionResult DeleteCartProduct(int id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var action = this.cartsService.DeleteProduct(id, this.User.Identity.Name);

                if (!action.Result)
                {
                    return this.Redirect("/");
                }

                return this.RedirectToAction(nameof(this.GetCart));
            }

            var sessionCart = SessionExtensions
               .GetDataObject<List<CartProductViewModel>>(this.HttpContext.Session, "shoppingCart");

            if (sessionCart == null)
            {
                return this.Redirect("/");
            }

            var itemToDelete = sessionCart.FirstOrDefault(p => p.Details.Id == id);

            if (itemToDelete == null)
            {
                return this.Redirect("/");
            }

            sessionCart.Remove(itemToDelete);

            SessionExtensions
                .SetDataObject<List<CartProductViewModel>>(this.HttpContext.Session, "shoppingCart", sessionCart);

            return this.RedirectToAction(nameof(this.GetCart));
        }

        public IActionResult Add(int id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var action = this.cartsService.AddProduct(id, this.User.Identity.Name);

                if (!action.Result)
                {
                    return this.Redirect("/");
                }

                return this.RedirectToAction(nameof(this.GetCart));
            }

            var sessionCart = SessionExtensions
               .GetDataObject<List<CartProductViewModel>>(this.HttpContext.Session, "shoppingCart");

            if (sessionCart == null)
            {
                sessionCart = new List<CartProductViewModel>();
            }

            var itemToAdd = this.shopProductsService.GetProductById<ShopProductViewModel>(id);

            if (itemToAdd == null)
            {
                return this.Redirect("/");
            }

            var sessionItem = new CartProductViewModel()
            {
                Quantity = 1,
                Details = itemToAdd,
            };

            sessionCart.Add(sessionItem);

            SessionExtensions
                .SetDataObject<List<CartProductViewModel>>(this.HttpContext.Session, "shoppingCart", sessionCart);

            return this.RedirectToAction(nameof(this.GetCart));
        }

        public IActionResult DeleteCart(int id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var action = this.cartsService.DeleteAll(this.User.Identity.Name);

                if (!action.Result)
                {
                    return this.RedirectToAction(nameof(this.GetCart));
                }

                return this.Redirect("/");
            }

            var sessionCart = SessionExtensions
               .GetDataObject<List<CartProductViewModel>>(this.HttpContext.Session, "shoppingCart");

            if (sessionCart == null)
            {
               return this.Redirect("/");
            }

            sessionCart = new List<CartProductViewModel>();

            SessionExtensions
                .SetDataObject<List<CartProductViewModel>>(this.HttpContext.Session, "shoppingCart", sessionCart);

            return this.Redirect("/");
        }

        public IActionResult FamilyCart()
        {
            return this.View();
        }
    }
}
