namespace FamilyKitchen.Web.Components
{
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class NavBarMealsViewComponent : ViewComponent
    {
        private readonly ICategoriesService categoriesService;

        public NavBarMealsViewComponent(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new CategoryListViewModel
            {
                CategoryNames =
                    this.categoriesService.GetAllMealCategories<CategoryViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
