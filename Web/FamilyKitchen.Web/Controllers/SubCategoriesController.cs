﻿namespace FamilyKitchen.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.SubCategories;
    using Microsoft.AspNetCore.Mvc;

    public class SubCategoriesController : BaseController
    {
        private readonly ISubCategoriesService subCategoriesService;
        private readonly ICategoriesService categoriesService;

        public SubCategoriesController(ISubCategoriesService subCategoriesService, ICategoriesService categoriesService)
        {
            this.subCategoriesService = subCategoriesService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Details(int id)
        {
            int categoryId = this.subCategoriesService.GetCategoryId(id);

            var viewModel = new DetailsSubCategoryViewModel
            {
                SubCategory = this.subCategoriesService.GetSubcategoryById(id),
                SubCategories = this.categoriesService.GetSubCategoriesByCategoryId(categoryId),
            };

            return this.View(viewModel);
        }
    }
}