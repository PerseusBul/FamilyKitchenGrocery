using FamilyKitchen.Web.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyKitchen.Web.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Index(OrderProfileInputModel input)
        {
            var oper = input;

            return this.NotFound();
        }

        public IActionResult Payment()
        {
            return this.View();
        }
    }
}
