namespace FamilyKitchen.Web.Components
{
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.ShoppingCarts;
    using Microsoft.AspNetCore.Mvc;

    public class CartTotalViewComponent : ViewComponent
    {
        private readonly IShoppingCartsService shoppingCartsService;

        public CartTotalViewComponent(IShoppingCartsService shoppingCartsService)
        {
            this.shoppingCartsService = shoppingCartsService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new CartTotalViewModel
            {
                Subtotal = 0,
                DeliveryPrice = 0,
                Discount = 0,
                Total = 0,
            };

            if (this.User.Identity.Name == null)
            {
                return this.View(model);
            }

            var viewModel = this.shoppingCartsService.GetCartTotalParameters(this.User.Identity.Name).Result;

            if (viewModel == null)
            {
                return this.View(model);
            }

            return this.View(viewModel);
        }
    }
}
