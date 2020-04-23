namespace FamilyKitchen.Web.Controllers
{
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.QuantityWish;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class QuantityWishController : ControllerBase
    {
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly UserManager<FamilyKitchenUser> userManager;

        public QuantityWishController(IShoppingCartsService shoppingCartsService, UserManager<FamilyKitchenUser> userManager)
        {
            this.shoppingCartsService = shoppingCartsService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<QuantityWishResponseModel>> Order(QuantityWishInputModel input)
        {
            var username = this.userManager.GetUserName(this.User);
            await this.shoppingCartsService.EditProduct(input.ShopProductId, username, input.UpOrder);
            var product = this.shoppingCartsService.GetProduct(input.ShopProductId, username);

            return new QuantityWishResponseModel { Quantity = (int)product.Quantity };
        }
    }
}
