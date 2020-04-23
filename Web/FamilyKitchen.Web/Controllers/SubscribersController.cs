namespace FamilyKitchen.Web.Controllers
{
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.Subscribers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class SubscribersController : ControllerBase
    {
        private readonly UserManager<FamilyKitchenUser> userManager;
        private readonly ISubscribersService subscribersService;

        public SubscribersController(UserManager<FamilyKitchenUser> userManager,
                                    ISubscribersService subscribersService)
        {
            this.userManager = userManager;
            this.subscribersService = subscribersService;
        }

        [HttpPost]
        public async Task<ActionResult<SubscribersResponseModel>> Subscribe(SubscribersInputModel input)
        {
            var username = this.userManager.GetUserName(this.User);
            var message = "Try again!";

            if (!this.ModelState.IsValid)
            {
                return new SubscribersResponseModel() { Message = message };
            }

            var action = await this.subscribersService.SubscribeEmail(username, input.Subscriber);

            if (action)
            {
                message = "Thank you!";
            }

            return new SubscribersResponseModel() { Message = message };
        }
    }
}
