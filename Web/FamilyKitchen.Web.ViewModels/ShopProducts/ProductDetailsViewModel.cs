namespace FamilyKitchen.Web.ViewModels.ShopProducts
{

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Mapping;

    public class ProductDetailsViewModel : IMapFrom<ShopProduct>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl => $"/images/product_{this.Id}.jpg";

        public string Producer { get; set; }

        public string Trademark { get; set; }

        public int Availability { get; set; }

        public bool Available => this.Availability > 0;

        // public string ExpireDate { get; set; }

        // public string NutritionDeclaration { get; set; }
    }
}
