namespace FamilyKitchen.Web.ViewModels.ShopProducts
{
    using System.Collections.Generic;

    using FamilyKitchen.Web.ViewModels.FoodResources;

    public class ListAllProductsViewModel
    {
        public IEnumerable<ShopProductViewModel> Products { get; set; }
    }
}
