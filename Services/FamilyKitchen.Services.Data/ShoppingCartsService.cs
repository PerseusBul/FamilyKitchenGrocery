﻿namespace FamilyKitchen.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using FamilyKitchen.Common;
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
                .Select(p => new CartProductViewModel()
                {
                    Quantity = p.Quantity,
                    Details = this.mapper.Map<ShopProductViewModel>(p.ShopProduct),
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
            var user = this.userRepository
                .All()
                .Where(x => x.UserName == username)
                .FirstOrDefault();

            var subTotal = this.GetSubtotal(username);
            var clientDiscount = (decimal)this.GetDiscountByCardId(user.ClientCardId);

            if (user == null || subTotal == null)
            {
                return null;
            }

            var delivery = (decimal)this.GetDeliveryPriceByCardId(user.ClientCardId);

            var subTotalOper = (decimal)subTotal;

            var discount =
                Math.Round(subTotalOper * (decimal)clientDiscount / GlobalConstants.PercentageDivider + user.ClientCard.Voucher, GlobalConstants.FractionalDigits);
            var total =
                Math.Round(subTotalOper - discount + delivery, GlobalConstants.FractionalDigits);

            if (total < 0)
            {
                total = 0;
            }

            var viewModel = new CartTotalViewModel
            {
                Subtotal = subTotalOper,
                DeliveryPrice = delivery,
                Discount = discount,
                Total = total,
            };

            return viewModel;
        }

        public async Task<bool> GetFamilyCart(FamilyKitchenUser moderator, FamilyKitchenUser member)
        {
            if (moderator == null || member == null)
            {
                return false;
            }

            if (moderator.UserName != member.UserName)
            {
                var moderatorCart = this.productCartRepository.All().Where(x => x.ShoppingCartId == moderator.ShoppingCartId).ToList();
                if (moderatorCart == null)
                {
                    return false;
                }

                var memberCart = moderatorCart.Select(mc => new ShoppingCartShopProduct
                {
                    ShoppingCartId = member.ShoppingCartId,
                    ShopProductId = mc.ShopProductId,
                    Quantity = mc.Quantity,
                }).ToList();

                foreach (var entity in memberCart)
                {
                    if (this.productCartRepository.All().Any(x => x.ShoppingCartId == entity.ShoppingCartId && x.ShopProductId == entity.ShopProductId))
                    {
                        this.productCartRepository.Update(entity);
                    }
                    else
                    {
                        await this.productCartRepository.AddAsync(entity);
                    }
                }

                await this.productCartRepository.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> ReturnFamilyCart(FamilyKitchenUser moderator, FamilyKitchenUser member)
        {
            if (moderator == null || member == null)
            {
                return false;
            }

            if (moderator.UserName != member.UserName)
            {
                var memberCart = this.productCartRepository.All().Where(x => x.ShoppingCartId == member.ShoppingCartId).ToList();
                if (memberCart == null)
                {
                    return false;
                }

                var moderatorCart = memberCart.Select(mc => new ShoppingCartShopProduct
                {
                    ShoppingCartId = moderator.ShoppingCartId,
                    ShopProductId = mc.ShopProductId,
                    Quantity = mc.Quantity,
                }).ToList();

                foreach (var entity in moderatorCart)
                {
                    if (this.productCartRepository.All().Any(x => x.ShoppingCartId == entity.ShoppingCartId && x.ShopProductId == entity.ShopProductId))
                    {
                        this.productCartRepository.Update(entity);
                    }
                    else
                    {
                        await this.productCartRepository.AddAsync(entity);
                    }
                }

                foreach (var entity in memberCart)
                {
                    this.productCartRepository.Delete(entity);
                }

                await this.productCartRepository.SaveChangesAsync();
            }

            return true;
        }

        private decimal? GetSubtotal(string username)
        {
            var productsAmounts = this.GetAllProducts(username);

            if (productsAmounts == null)
            {
                return null;
            }

            var subTotal = productsAmounts
                .Select(x => x.CartProductTotal)
                .Sum();

            return subTotal;
        }

        private decimal? GetDiscountByCardId(string cardId)
        {
            var card = this.clientCardRepository
                .All()
                .Where(c => c.Id == cardId)
                .FirstOrDefault();

            return card?.Discount;
        }

        private decimal? GetDeliveryPriceByCardId(string cardId)
        {
            var card = this.clientCardRepository
                .All()
                .Where(c => c.Id == cardId)
                .FirstOrDefault();

            return card?.DeliveryPrice;
        }
    }
}
