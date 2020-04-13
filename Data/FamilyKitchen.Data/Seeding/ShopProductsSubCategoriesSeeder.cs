namespace FamilyKitchen.Data.Seeding
{
    using FamilyKitchen.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    internal class ShopProductsSubCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ShopProductsSubCategories.Any())
            {
                return;
            }

            var jsonString = File.ReadAllText("../FamilyKitchen.Web/wwwroot/seedData/shopProductsSubCategories.json");
            var entities = JsonSerializer.Deserialize<List<ShopProductSubCategory>>(jsonString);

            await dbContext.AddRangeAsync(entities);
        }
    }
}
