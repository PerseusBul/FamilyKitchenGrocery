namespace FamilyKitchen.Web.Components
{
    using FamilyKitchen.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class CheckOutTotalViewComponent : ViewComponent
    {
        private readonly IShoppingCartsService shoppingCartsService;

        public CheckOutTotalViewComponent(IShoppingCartsService shoppingCartsService)
        {
            this.shoppingCartsService = shoppingCartsService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = this.shoppingCartsService.GetCartTotalParameters(this.User.Identity.Name).Result;

            return this.View(viewModel);
        }
    }
}
