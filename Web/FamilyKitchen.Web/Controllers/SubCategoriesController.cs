namespace FamilyKitchen.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Services.Data;

    public class SubCategoriesController : BaseController
    {
        private readonly ISubCategoriesService subCategoriesService;

        public SubCategoriesController(ISubCategoriesService subCategoriesService)
        {
            this.subCategoriesService = subCategoriesService;
        }
    }
}
