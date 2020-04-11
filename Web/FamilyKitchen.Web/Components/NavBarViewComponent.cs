namespace FamilyKitchen.Web.Components
{

    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class NavBarViewComponent : ViewComponent
    {
        private readonly ICategoriesService categoriesService;

        public NavBarViewComponent(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new CategoryListViewModel
            {
                CategoryNames =
                    this.categoriesService.GetAllProductCategories<CategoryViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
