namespace FamilyKitchen.Web.ViewModels.ShopProducts
{
    using System.Collections.Generic;

    using FamilyKitchen.Web.ViewModels.FoodResources;

    public class ListAllMealsViewModel
    {
        public IEnumerable<MealViewModel> Products { get; set; }
    }
}
