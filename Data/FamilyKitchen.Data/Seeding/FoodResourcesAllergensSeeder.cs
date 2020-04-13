namespace FamilyKitchen.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;

    internal class FoodResourcesAllergensSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.FoodResourcesAllergens.Any())
            {
                return;
            }

            var jsonString = File.ReadAllText("../FamilyKitchen.Web/wwwroot/seedData/foodResourcesAllergens.json");
            var entities = JsonSerializer.Deserialize<List<FoodResourceAllergen>>(jsonString);

            await dbContext.AddRangeAsync(entities);
        }
    }
}
