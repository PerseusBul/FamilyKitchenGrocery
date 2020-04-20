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
        private readonly IDeletableEntityRepository<ClientCard> clientCardRepository;
        private readonly IMapper mapper;

        public ShoppingCartsService(IDeletableEntityRepository<ShoppingCart> cartRepository,
                                    IDeletableEntityRepository<FamilyKitchenUser> userRepository,
                                    IDeletableEntityRepository<ShopProduct> productRepository,
                                    IRepository<ShoppingCartShopProduct> productCartRepository,
                                    IDeletableEntityRepository<ClientCard> clientCardRepository,
                                    IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.userRepository = userRepository;
            this.productRepository = productRepository;
            this.productCartRepository = productCartRepository;
            this.clientCardRepository = clientCardRepository;
            this.mapper = mapper;
        }

        public async Task<bool> AddProduct(int id, string username, decimal quantity = 0)
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

            if (quantity <= 0)
            {
                quantity = 1;
            }

            var cartProduct = new ShoppingCartShopProduct()
            {
                ShopProductId = product.Id,
                ShoppingCartId = user.ShoppingCartId,
                Quantity = quantity,
            };

            await this.productCartRepository.AddAsync(cartProduct);
            await this.productCartRepository.SaveChangesAsync();

            return true;
        }

        public void AddSessionCart(List<CartProductViewModel> session, string username)
        {
            var user = this.userRepository.All().Where(x => x.UserName == username).FirstOrDefault();
            var entities = new List<ShoppingCartShopProduct>();

            foreach (var product in session)
            {
                var cartProduct = new ShoppingCartShopProduct()
                {
                    ShopProductId = product.Details.Id,
                    ShoppingCartId = user.ShoppingCartId,
                    Quantity = product.Quantity,
                };

                entities.Add(cartProduct);
            }

            this.productCartRepository.AddRangeAsync(entities).GetAwaiter().GetResult();
            this.productCartRepository.SaveChangesAsync().GetAwaiter().GetResult();
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

        public async Task EditProduct(int id, string username, bool upOrder)
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

            if (upOrder)
            {
                cartProduct.Quantity++;
            }
            else
            {
                if (cartProduct.Quantity > 1)
                {
                    cartProduct.Quantity--;
                }
            }

            this.productCartRepository.Update(cartProduct);
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

        public async Task<bool> DeleteAll(string username)
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

                await this.productCartRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<CartTotalViewModel> GetCartTotalParameters(string username)
        {
            if (username == null)
            {
                var model = new CartTotalViewModel()
                {
                    Subtotal = 0,
                    DeliveryPrice = 0,
                    Discount = 0,
                    Total = 0,
                };

                return model;
            }

            var user = this.userRepository.All().Where(x => x.UserName == username).FirstOrDefault();
            var subTotal = this.GetSubtotal(username);
            var clientDiscount = this.GetDiscountByCardId(user.ClientCardId);

            if (user == null || subTotal == null || clientDiscount == null)
            {
                return null;
            }

            var subTotalOper = (decimal)subTotal;
            var discount = Math.Round(subTotalOper * (decimal)clientDiscount / 100, 2);
            var total = Math.Round(subTotalOper - discount + 0, 2);

            var viewModel = new CartTotalViewModel()
            {
                Subtotal = subTotalOper,
                DeliveryPrice = 0,
                Discount = discount,
                Total = total,
            };

            return viewModel;
        }

        private decimal? GetSubtotal(string username)
        {
            var productsAmounts = this.GetAllProducts(username);

            if (productsAmounts == null)
            {
                return null;
            }

            var subTotal = productsAmounts.Select(x => x.CartProductTotal).Sum();

            return subTotal;
        }

        private decimal? GetDiscountByCardId(string cardId)
        {
            var card = this.clientCardRepository.All().Where(c => c.Id == cardId).FirstOrDefault();

            return card?.Discount;
        }
    }
}
