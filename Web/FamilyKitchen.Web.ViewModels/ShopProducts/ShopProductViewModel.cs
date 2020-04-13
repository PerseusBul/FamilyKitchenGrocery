namespace FamilyKitchen.Web.ViewModels.ShopProducts
{
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Mapping;
    using System;

    public class ShopProductViewModel : IMapFrom<ShopProduct>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public string ImageUrl => $"/images/product_{this.Id}.jpg";

        public decimal SalePrice => Math.Round(this.Price - (this.Price * this.Discount / 100), 2);

    }
}
