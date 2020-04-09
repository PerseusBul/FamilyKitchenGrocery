namespace FamilyKitchen.Web.ViewModels.ShopProducts
{
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Mapping;

    // TODO change recipe to shopProduct

    public class MealViewModel : IMapFrom<ShopProduct>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl => $"/images/meal_{this.Id}.jpg";
    }
}
