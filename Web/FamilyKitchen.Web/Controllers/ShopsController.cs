﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyKitchen.Web.Controllers
{
    public class ShopsController : BaseController
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}