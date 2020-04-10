namespace FamilyKitchen.Web.ViewModels.ShopProducts
{
    using System.Collections.Generic;

    using FamilyKitchen.Web.ViewModels.FoodResources;

    public class ListAllMealsViewModel
    {
        public IEnumerable<ShopProductViewModel> Products { get; set; }
    }
}
