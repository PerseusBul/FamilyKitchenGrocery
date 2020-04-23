namespace FamilyKitchen.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Mapping;

    public class FavoriteProductService : IFavoriteProductService
    {
        private readonly IDeletableEntityRepository<FamilyKitchenUser> userRepository;
        private readonly IRepository<FamilyKitchenUserFavoriteProduct> favoritesRepository;
        private readonly IDeletableEntityRepository<ShopProduct> productRepository;

        public FavoriteProductService(IDeletableEntityRepository<FamilyKitchenUser> userRepository,
                                      IRepository<FamilyKitchenUserFavoriteProduct> favoritesRepository,
                                      IDeletableEntityRepository<ShopProduct> productRepository)
        {
            this.userRepository = userRepository;
            this.favoritesRepository = favoritesRepository;
            this.productRepository = productRepository;
        }

        public IEnumerable<T> ListAll<T>(string username)
        {
            var user = this.userRepository.All().Where(u => u.UserName == username).FirstOrDefault();

            IQueryable<ShopProduct> query =
                this.favoritesRepository
                .All()
                .Where(x => x.FamilyKitchenUserId == user.Id)
                .Select(x => x.ShopProduct);

            return query.To<T>().ToList();
        }

        public async Task<bool> Add(int id, string username)
        {
            bool result = false;
            var user = this.userRepository.All().Where(u => u.UserName == username).FirstOrDefault();
            var product = this.productRepository.All().Where(p => p.Id == id).FirstOrDefault();

            if (user == null || product == null)
            {
                return result;
            }

            var favoriteProduct = new FamilyKitchenUserFavoriteProduct
            {
                FamilyKitchenUserId = user.Id,
                ShopProductId = product.Id,
            };

            if (this.favoritesRepository
                .All()
                .Where(fp => fp.FamilyKitchenUserId == user.Id && fp.ShopProductId == product.Id)
                .FirstOrDefault()
                != null)
            {
                return result;
            }

            await this.favoritesRepository.AddAsync(favoriteProduct);
            await this.favoritesRepository.SaveChangesAsync();

            result = true;

            return result;
        }

        public async Task<bool> Delete(int id, string username)
        {
            bool result = false;
            var user = this.userRepository.All().Where(u => u.UserName == username).FirstOrDefault();
            var product = this.productRepository.All().Where(p => p.Id == id).FirstOrDefault();

            if (user == null || product == null)
            {
                return result;
            }

            var favoriteProduct = this.favoritesRepository
                .All()
                .Where(fp => fp.FamilyKitchenUserId == user.Id && fp.ShopProductId == product.Id)
                .FirstOrDefault();

            if (favoriteProduct == null)
            {
                return result;
            }

            this.favoritesRepository.Delete(favoriteProduct);
            await this.favoritesRepository.SaveChangesAsync();

            result = true;

            return result;
        }
    }
}
