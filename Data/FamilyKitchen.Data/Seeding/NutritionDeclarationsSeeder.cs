namespace FamilyKitchen.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;

    internal class NutritionDeclarationsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.NutritionDeclarations.Any())
            {
                return;
            }

            var jsonString = File.ReadAllText("../FamilyKitchen.Web/wwwroot/seedData/nutritionDeclarations.json");
            var entities = JsonSerializer.Deserialize<List<NutritionDeclaration>>(jsonString);

            await dbContext.NutritionDeclarations.AddRangeAsync(entities);
        }
    }
}
