namespace FamilyKitchen.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;

    internal class AllergensSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Allergens.Any())
            {
                return;
            }

            var jsonString = File.ReadAllText("../FamilyKitchen.Web/wwwroot/seedData/allergens.json");
            var entities = JsonSerializer.Deserialize<List<Allergen>>(jsonString);

            await dbContext.AddRangeAsync(entities);
        }
    }
}
