namespace FamilyKitchen.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;

    internal class SubCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.SubCategories.Any())
            {
                return;
            }

            var jsonString = File.ReadAllText("../FamilyKitchen.Web/wwwroot/seedData/subCategories.json");
            var entities = JsonSerializer.Deserialize<List<SubCategory>>(jsonString);

            await dbContext.AddRangeAsync(entities);
        }
    }
}
