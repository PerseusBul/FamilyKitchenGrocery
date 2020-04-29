namespace FamilyKitchen.Web.ApiControllers
{
    using System.Threading.Tasks;

    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.ClientCards;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class VoucherController : ControllerBase
    {
        private readonly IClientCardsService cardService;

        public VoucherController(IClientCardsService cardService)
        {
            this.cardService = cardService;
        }

        [HttpPost]
        public async Task<ActionResult<VoucherResponseModel>> Accept(VoucherInputModel input)
        {
            var isValid = this.cardService.IsValid(input.NumberCode);

            var answer = new VoucherResponseModel
            {
                Message = string.Empty,
            };

            if (!isValid)
            {
                answer.Message = "This voucher is not valid!";

                return answer;
            }

            await this.cardService.ApplyVoucher(this.User.Identity.Name, input.NumberCode);

            answer.Message = "Your voucher was accepted!";

            return answer;
        }
    }
}
