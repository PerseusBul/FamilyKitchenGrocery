namespace FamilyKitchen.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using FamilyKitchen.Common;
    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Data.Models.Enums;
    using FamilyKitchen.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Http;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<FamilyKitchenUser> usersRepository;
        private readonly IRepository<ShoppingCartShopProduct> cartProductRepository;
        private readonly IDeletableEntityRepository<ClientCard> clientCardRepository;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly IMapper mapper;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepository,
                             IDeletableEntityRepository<FamilyKitchenUser> usersRepository,
                             IRepository<ShoppingCartShopProduct> cartProductRepository,
                             IDeletableEntityRepository<ClientCard> clientCardRepository,
                             IShoppingCartsService shoppingCartsService,
                             IMapper mapper
                           )
        {
            this.ordersRepository = ordersRepository;
            this.usersRepository = usersRepository;
            this.cartProductRepository = cartProductRepository;
            this.clientCardRepository = clientCardRepository;
            this.shoppingCartsService = shoppingCartsService;
            this.mapper = mapper;
        }

        public async Task<bool> CreateOrder(string username, OrderProfileInputModel profile, string paymentMethod)
        {
            var user = this.usersRepository
                .All()
                .Where(u => u.UserName == username)
                .FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            var order = this.OrderFactory(user, profile, paymentMethod);

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();

            var products = this.cartProductRepository
                .All()
                .Where(x => x.ShoppingCartId == user.ShoppingCartId)
                .AsQueryable();

            await this.OrderEntityUpdate(user, order, products);

            foreach (var entity in products)
            {
                this.cartProductRepository.Delete(entity);
            }

            await this.cartProductRepository.SaveChangesAsync();

            return true;
        }

        private Order OrderFactory(FamilyKitchenUser user, OrderProfileInputModel profile, string paymentMethod)
        {
            var card = this.clientCardRepository
                .All()
                .Where(x => x.Id == user.ClientCardId)
                .FirstOrDefault();

            var clientCard = user.ClientCard?.DeliveryPrice;

            var order = new Order()
            {
                Status = OrderStatus.PayingAwaiting,
                PaymentStatus = PaymentStatus.Pending,
                PaymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), paymentMethod),
                OrderDate = DateTime.UtcNow,
                DeliveryDate = DateTime.UtcNow.AddDays(2),
                DeliveryPrice = card.DeliveryPrice,
                FamilyKitchenUser = user,
                OrderProfile = this.mapper.Map<OrderProfile>(profile),
                TotalPrice = 0,
            };

            return order;
        }

        private async Task OrderEntityUpdate(FamilyKitchenUser user, Order order, IQueryable<ShoppingCartShopProduct> products)
        {
            var productList = products.Select(x => new OrderShopProduct
            {
                OrderId = order.Id,
                ShopProductId = x.ShopProductId,
                Quantity = x.Quantity,
                Price = Math.Round(x.ShopProduct.Price - (x.ShopProduct.Price * x.ShopProduct.Discount / GlobalConstants.PercentageDivider), GlobalConstants.FractionalDigits),
            })
            .ToList();

            var card = this.clientCardRepository
                .All()
                .Where(x => x.Id == user.ClientCardId)
                .FirstOrDefault();

            var discount = card.Discount;
            var subTotal = productList.Sum(x => x.Price * x.Quantity);
            var totalPrice = Math.Round(subTotal - (subTotal * discount / GlobalConstants.PercentageDivider) + order.DeliveryPrice, GlobalConstants.FractionalDigits);

            if (totalPrice<0)
            {
                totalPrice = 0;
                order.PaymentStatus = PaymentStatus.Complete;
                order.Status = OrderStatus.Paid;
            }

            order.OrdersShopProducts = productList;
            order.TotalPrice = totalPrice;

            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();

            card.Voucher = 0;
            card.DeliveryPrice = 10;

            this.clientCardRepository.Update(card);
            await this.clientCardRepository.SaveChangesAsync();
        }
    }
}
