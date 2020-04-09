namespace FamilyKitchen.Web.ViewModels.ShopProducts
{
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Mapping;

    public class ShopProductViewModel : IMapFrom<ShopProduct>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl => $"/images/product_{this.Id}.jpg";
    }
}
