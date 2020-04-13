namespace FamilyKitchen.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;

    internal class FoodResourcesRecipesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.FoodResourcesRecipes.Any())
            {
                return;
            }

            var jsonString = File.ReadAllText("../FamilyKitchen.Web/wwwroot/seedData/foodResourcresRecipes.json");
            var entities = JsonSerializer.Deserialize<List<FoodResourceRecipe>>(jsonString);

            await dbContext.AddRangeAsync(entities);
        }
    }
}
