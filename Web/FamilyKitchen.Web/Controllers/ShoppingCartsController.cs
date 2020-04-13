using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyKitchen.Web.Controllers
{
    public class ShoppingCartsController : BaseController
    {
        public ShoppingCartsController()
        {

        }

        public IActionResult GetCart()
        {
            return this.View();
        }
    }
}
