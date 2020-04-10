namespace FamilyKitchen.Web.ViewModels.SubCategories
{
    using System.Collections.Generic;
    using System.Linq;

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Mapping;
    using FamilyKitchen.Web.ViewModels.ShopProducts;

    public class SubCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ShopProductViewModel> Products { get; set; }
    }
}