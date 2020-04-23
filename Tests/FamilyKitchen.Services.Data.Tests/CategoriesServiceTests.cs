namespace FamilyKitchen.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using FamilyKitchen.Data;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Data.Repositories;
    using FamilyKitchen.Web.MappingConfiguration;
    using FamilyKitchen.Web.ViewModels.SubCategories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public async Task CorrectInputToGettingByNameShouldReturnTrue()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new ApplicationDbContext(builder.Options);

            var categoriesRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var subCategoriesRepository = new EfDeletableEntityRepository<SubCategory>(dbContext);

            var profile = new FamilyKitchenProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var categoriesService = new CategoriesService(categoriesRepository, subCategoriesRepository, mapper);

            await categoriesRepository.AddAsync(new Category
            {
                Name = "Drinks",
                SubCategories = new List<SubCategory>
                {
                    new SubCategory { Name = "Alcohol", Category = new Category { Name = "Drinks", }, },
                    new SubCategory { Name = "Water", Category = new Category { Name = "Drinks", }, },
                },
            });

            await categoriesRepository.SaveChangesAsync();

            var category = categoriesService.GetByName("Drinks");

            Assert.Equal("Drinks", category.Name);
        }

        [Fact]
        public async Task CorrectInputToGettingByCategoryIdShouldReturnCorrectResult()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new ApplicationDbContext(builder.Options);

            var categoriesRepository = new EfDeletableEntityRepository<Category>(dbContext);
            var subCategoriesRepository = new EfDeletableEntityRepository<SubCategory>(dbContext);

            var profile = new FamilyKitchenProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var categoriesService = new CategoriesService(categoriesRepository, subCategoriesRepository, mapper);

            var category = new Category
            {
                Id = 2,
                Name = "Drinks",
            };

            await categoriesRepository.AddAsync(category);

            await categoriesRepository.SaveChangesAsync();

            await subCategoriesRepository.AddRangeAsync(new List<SubCategory>
            {
                new SubCategory { Id = 1, Name = "Alcohol", Category = category, },
                new SubCategory { Id = 2, Name = "Water", Category = category, },
            });

            await subCategoriesRepository.SaveChangesAsync();

            var categoryId = 2;

            var subCategories = (List<SubCategoryViewModel>)categoriesService.GetSubCategoriesByCategoryId(categoryId);

            Assert.Equal(2, subCategories.Count);
        }
    }
}
