namespace FamilyKitchen.Web.Controllers
{
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.SubCategories;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Details(int id)
        {
            var viewModel = new ListAllSubCategoriesViewModel()
            {
                SubCategories = this.categoriesService.GetSubCategoriesByCategoryId(id),
            };

            return this.View(viewModel);
        }
    }
}
