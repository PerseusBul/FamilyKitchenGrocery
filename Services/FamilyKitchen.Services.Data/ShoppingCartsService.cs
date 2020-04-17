namespace FamilyKitchen.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Mapping;
    using FamilyKitchen.Web.ViewModels.ShoppingCarts;
    using FamilyKitchen.Web.ViewModels.ShopProducts;

    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly IDeletableEntityRepository<ShoppingCart> cartRepository;
        private readonly IDeletableEntityRepository<FamilyKitchenUser> userRepository;
        private readonly IDeletableEntityRepository<ShopProduct> productRepository;
        private readonly IRepository<ShoppingCartShopProduct> productCartRepository;
        private readonly IMapper mapper;

        public ShoppingCartsService(IDeletableEntityRepository<ShoppingCart> cartRepository,
                                    IDeletableEntityRepository<FamilyKitchenUser> userRepository,
                                    IDeletableEntityRepository<ShopProduct> productRepository,
                                    IRepository<ShoppingCartShopProduct> productCartRepository,
                                    IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.userRepository = userRepository;
            this.productRepository = productRepository;
            this.productCartRepository = productCartRepository;
            this.mapper = mapper;
        }

        public async Task<bool> AddProduct(int id, string username)
        {
            var user = this.userRepository.All().Where(x => x.UserName == username).FirstOrDefault();
            var product = this.productRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (user == null || product == null)
            {
                return false;
            }

            var isExist = this.productCartRepository
                .All()
                .Any(y => y.ShopProductId == product.Id
                                          && y.ShoppingCartId == user.ShoppingCartId);

            if (isExist)
            {
                return false;
            }

            var cartProduct = new ShoppingCartShopProduct()
            {
                ShopProductId = product.Id,
                ShoppingCartId = user.ShoppingCartId,
                Quantity = 1,
            };

            await this.productCartRepository.AddAsync(cartProduct);
            await this.productCartRepository.SaveChangesAsync();

            return true;
        }

        public ShoppingCartShopProduct GetProduct(int id, string username)
        {
            var user = this.userRepository.All().Where(x => x.UserName == username).FirstOrDefault();
            var product = this.productRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (user == null || product == null)
            {
                return null;
            }

            var cartProduct = this.productCartRepository
                .All()
                .Where(y => y.ShopProductId == product.Id
                                          && y.ShoppingCartId == user.ShoppingCartId)
                .FirstOrDefault();

            return cartProduct;
        }

        public async Task<bool> DeleteProduct(int id, string username)
        {
            var user = this.userRepository.All().Where(x => x.UserName == username).FirstOrDefault();
            var product = this.productRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (user == null || product == null)
            {
                return false;
            }

            var cartProduct = this.productCartRepository
                .All()
                .Where(y => y.ShopProductId == product.Id
                                          && y.ShoppingCartId == user.ShoppingCartId)
                .FirstOrDefault();

            if (cartProduct == null)
            {
                return false;
            }

            this.productCartRepository.Delete(cartProduct);
            await this.productCartRepository.SaveChangesAsync();

            return true;
        }

        public async Task EditProduct(int id, string username, int quantity)
        {
            var user = this.userRepository.All().Where(x => x.UserName == username).FirstOrDefault();
            var product = this.productRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (user == null || product == null)
            {
                return;
            }

            var cartProduct = this.productCartRepository
                .All()
                .Where(y => y.ShopProductId == product.Id
                                          && y.ShoppingCartId == user.ShoppingCartId)
                .FirstOrDefault();

            if (cartProduct == null)
            {
                return;
            }

            if (quantity <= 0)
            {
                quantity = 1;
            }

            cartProduct.Quantity = quantity;

            await this.productCartRepository.AddAsync(cartProduct);
            await this.productCartRepository.SaveChangesAsync();
        }

        public IEnumerable<CartProductViewModel> GetAllProducts(string username)
        {
            var user = this.userRepository.All().Where(x => x.UserName == username).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var result =
                this.productCartRepository
                .All()
                .Where(cp => cp.ShoppingCartId == user.ShoppingCartId)
                .Select(cp => new CartProductViewModel()
                {
                    Quantity = cp.Quantity,
                    Details = this.mapper.Map<ShopProductViewModel>(cp.ShopProduct),
                })
                .ToList();

            return result;
        }

        public bool DeleteAll(string username)
        {
            var user = this.userRepository.All().Where(x => x.UserName == username).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            try
            {
                this.productCartRepository
                .All()
                .Where(cp => cp.ShoppingCartId == user.ShoppingCartId)
                .ToList()
                .ForEach(entity => this.productCartRepository.Delete(entity));

                this.productCartRepository.SaveChangesAsync().GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
