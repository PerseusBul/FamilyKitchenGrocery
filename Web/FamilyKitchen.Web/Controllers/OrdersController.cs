namespace FamilyKitchen.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ExtensionsConf.SessionConf;
    using FamilyKitchen.Web.ViewModels.Orders;
    using FamilyKitchen.Web.ViewModels.ShoppingCarts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

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
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var sessionOrder = input;

            ExtensionsConf.SessionConf.SessionExtensions
               .SetDataObject<OrderProfileInputModel>(this.HttpContext.Session, "sessionOrder", sessionOrder);

            return this.RedirectToAction(nameof(PaymentMethod));
        }

        public IActionResult PaymentMethod()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult PaymentMethod(OrderPaymentInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            //var sessionPayWay = input;

            //SessionExtensions
            //   .SetDataObject<OrderPaymentInputModel>(this.HttpContext.Session, "sessionPayWay", sessionPayWay);

            var sessionOrderProfile = ExtensionsConf.SessionConf.SessionExtensions
                .GetDataObject<OrderProfileInputModel>(this.HttpContext.Session, "sessionOrder");

            if (sessionOrderProfile==null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var actionOrder = this.ordersService
                .CreateOrder(this.User.Identity.Name, sessionOrderProfile, input.PaymentMethod);

            if (!actionOrder.Result)
            {
                return this.RedirectToAction(nameof(Index));
            }

            //this.session.Clear();

            return this.RedirectToAction(nameof(PayArea));
        }

        public IActionResult PayArea()
        {
            return this.View();
        }
    }
}
