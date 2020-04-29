namespace FamilyKitchen.Web.Controllers.ApiControllers
{
    using System.Threading.Tasks;

    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.ClientCards;
    using FamilyKitchen.Web.ViewModels.ShoppingCarts;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ShippingTaxController : ControllerBase
    {
        private readonly IClientCardsService cardService;
        private readonly IShoppingCartsService shoppingCartsService;

        public ShippingTaxController(IClientCardsService cardService, IShoppingCartsService shoppingCartsService)
        {
            this.cardService = cardService;
            this.shoppingCartsService = shoppingCartsService;
        }

        [HttpPost]
        public async Task<ActionResult<CartTotalViewModel>> SetDestination(ShippingTaxInputModel input)
        {
            var deliveryTax = this.cardService.DeliveryPriceCalculator(input.City, input.Block);

            await this.cardService.ApplyDeliveryPrice(this.User.Identity.Name, deliveryTax);
            var result = await this.shoppingCartsService.GetCartTotalParameters(this.User.Identity.Name);

            return result;
        }
    }
}
