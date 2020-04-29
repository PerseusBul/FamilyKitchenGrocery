namespace FamilyKitchen.Web.Controllers
{
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Index(OrderProfileInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            var sessionOrder = input;

            ExtensionsConf.SessionConf.SessionExtensions
               .SetDataObject<OrderProfileInputModel>(this.HttpContext.Session, "sessionOrder", sessionOrder);

            return this.RedirectToAction(nameof(this.PaymentMethod));
        }

        public IActionResult PaymentMethod()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> PaymentMethod(OrderPaymentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            // var sessionPayWay = input;

            // SessionExtensions
            //   .SetDataObject<OrderPaymentInputModel>(this.HttpContext.Session, "sessionPayWay", sessionPayWay);
            var sessionOrderProfile = ExtensionsConf.SessionConf.SessionExtensions
                .GetDataObject<OrderProfileInputModel>(this.HttpContext.Session, "sessionOrder");

            if (sessionOrderProfile == null)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            var actionOrder = await this.ordersService
                .CreateOrder(this.User.Identity.Name, sessionOrderProfile, input.PaymentMethod);

            if (!actionOrder)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.RedirectToAction(nameof(this.PayArea));
        }

        public IActionResult PayArea()
        {
            return this.View();
        }
    }
}
