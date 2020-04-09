namespace FamilyKitchen.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Data.Models.Enums;
    using FamilyKitchen.Services.Mapping;

    public class ShopProductsService : IShopProductsService
    {
        private readonly IDeletableEntityRepository<ShopProduct> productsRepository;
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;

        public ShopProductsService(IDeletableEntityRepository<ShopProduct> productsRepository, 
            IDeletableEntityRepository<Recipe> recipeRepository)
        {
            this.productsRepository = productsRepository;
            this.recipeRepository = recipeRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ShopProduct> query =
                this.productsRepository.All();

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllMeals<T>()
        {
            IQueryable<ShopProduct> query =
               this.productsRepository.All().Where(x => x.RecipeId != null);

            IQueryable<Recipe> recipes =
               this.recipeRepository.All();

            if (query.Count() < recipes.Count())
            {
                var newRecipes = recipes.Where(x => !query.Any(y => y.RecipeId == x.Id)).AsQueryable();

                var newMeals = this.ProduceNewKitchenProducts(newRecipes);

                this.productsRepository.AddRangeAsync(newMeals).GetAwaiter().GetResult();
                this.productsRepository.SaveChangesAsync().GetAwaiter().GetResult();

                query =
               this.productsRepository.All().Where(x => x.RecipeId != null);
            }

            return query.Where(x => x.Recipe.IsPrivate == false).To<T>().ToList();
        }

        public IEnumerable<T> GetAllProducts<T>()
        {
            IQueryable<ShopProduct> query =
                this.productsRepository.All().Where(x => x.RecipeId == null);

            return query.To<T>().ToList();
        }

        public T GetProductById<T>(int id)
        {
            var product = this.productsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return product;
        }

        private IEnumerable<ShopProduct> ProduceNewKitchenProducts(IQueryable<Recipe> meals)
        {
            Random number = new Random();
            var debug = meals.Select(x => x.FoodResourcesRecipes.Select(y => y.FoodResource.Price * y.Quantity).Sum()).ToList();

            var products = meals.Select(x => new ShopProduct
            {
                Name = x.Name,
                Price = Math.Round(x.FoodResourcesRecipes.Select(y => y.FoodResource.Price * y.Quantity).Sum() * 1.2m, 2),
                Availability = x.IsPrivate == true ? (int)x.Size : (int)x.Size * 10,
                ExpireDate = DateTime.UtcNow.AddDays(30),
                MetricSystemUnit = MetricSystemUnit.kg,
                EANCode = ulong.Parse($"380000{number.Next(1000000, 9999999)}"),
                Producer = "Family Kitchen",
                TradeMark = "Happy Meal",
                RecipeId = x.Id,
            }).ToList();

            return products;
        }
    }
}
