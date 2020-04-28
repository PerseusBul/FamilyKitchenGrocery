namespace FamilyKitchen.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Data.Models.Enums;
    using FamilyKitchen.Services.Mapping;
    using Nest;

    public class ShopProductsService : IShopProductsService
    {
        private readonly IDeletableEntityRepository<ShopProduct> productsRepository;
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;
        private readonly IElasticClient elasticClient;

        public ShopProductsService(IDeletableEntityRepository<ShopProduct> productsRepository, 
                                   IDeletableEntityRepository<Recipe> recipeRepository,
                                   IElasticClient elasticClient)
        {
            this.productsRepository = productsRepository;
            this.recipeRepository = recipeRepository;
            this.elasticClient = elasticClient;
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
               this.productsRepository.All().Where(x => x.RecipeId != null && x.Recipe.IsPrivate == false);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllProducts<T>()
        {
            IQueryable<ShopProduct> query =
                this.productsRepository.All().Where(x => x.RecipeId == null);

            // ElasticSearch
            // var products = query.Skip(10).ToArray();
            // var indexManyResponse = this.elasticClient.IndexMany(products);
            // var result = this.elasticClient.Bulk(b => b.Index("shopproducts").IndexMany(products));
            return query.To<T>().ToList();
        }

        public T GetProductById<T>(int id)
        {
            var product = this.productsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return product;
        }

        public T GetProductByName<T>(string name)
        {
            var product = this.productsRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();
            return product;
        }

        public async Task<bool> Add(ShopProduct product)
        {
            var isExist = this.productsRepository
                .All()
                .Any(p => p.Name == product.Name && p.EANCode == product.EANCode);

            if (isExist || product == null)
            {
                return false;
            }

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            // ElasticSearch
            await this.elasticClient.IndexDocumentAsync<ShopProduct>(product);

            return true;
        }

        public async Task Delete(ShopProduct product)
        {
            var isExist = this.productsRepository
                .All()
                .Any(p => p.Name == product.Name || p.EANCode == product.EANCode);

            if (isExist || product == null)
            {
                return;
            }

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            // ElasticSearch
            await this.elasticClient.DeleteAsync<ShopProduct>(product);
        }

        public async Task Update(ShopProduct product)
        {
            var isExist = this.productsRepository
                .All()
                .Any(p => p.Name == product.Name || p.EANCode == product.EANCode);

            if (isExist || product == null)
            {
                return;
            }

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            // ElasticSearch
            await this.elasticClient.UpdateAsync<ShopProduct>(product, u => u.Doc(product));
        }

        public IEnumerable<ShopProduct> ProduceNewKitchenProducts(IQueryable<Recipe> meals)
        {
            Random number = new Random();

            var products = meals.Select(x => new ShopProduct
            {
                Name = x.Name,
                Price = Math.Round(x.FoodResourcesRecipes.Select(y => y.FoodResource.Price * y.Quantity).Sum() * 3m, 2),
                Availability = x.IsPrivate == true ? (int)x.Size : (int)x.Size * 10,
                ExpireDate = DateTime.UtcNow.AddDays(30),
                MetricSystemUnit = MetricSystemUnit.kg,
                EANCode = ulong.Parse($"380000{number.Next(1000000, 9999999)}"),
                Producer = "Family Kitchen",
                TradeMark = "Happy Meal",
                RecipeId = x.Id,
            }).ToList();

            // ElasticSearch
            var indexManyResponse = this.elasticClient.IndexMany(products);

            return products;
        }
    }
}
