namespace FamilyKitchen.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FamilyKitchen.Data;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Data.Repositories;
    using FamilyKitchen.Web.ViewModels.ShopProducts;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class FavoriteProductServiceTests
    {
        [Fact]
        public async Task CorrectInputShouldReturnsTrue()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new ApplicationDbContext(builder.Options);

            var userRepository = new EfDeletableEntityRepository<FamilyKitchenUser>(dbContext);
            var favoritesRepository = new EfRepository<FamilyKitchenUserFavoriteProduct>(dbContext);
            var productRepository = new EfDeletableEntityRepository<ShopProduct>(dbContext);

            var favoritesService = new FavoriteProductService(userRepository, favoritesRepository, productRepository);

            await userRepository.AddAsync(new FamilyKitchenUser
            {
                UserName = "qsen.peichev@gmail.com",
                Email = "qsen.peichev@gmail.com",
                ClientCard = new ClientCard(),
                ShoppingCart = new ShoppingCart(),
            });

            await userRepository.SaveChangesAsync();

            await productRepository.AddAsync(new ShopProduct { Id = 2, Name = "boza", Price = 2m, Discount = 0, });
            await productRepository.SaveChangesAsync();

            var actAdd = favoritesService.Add(2, "qsen.peichev@gmail.com");
            var actDel = favoritesService.Delete(2, "qsen.peichev@gmail.com");

            Assert.True(actAdd.Result);
            Assert.True(actDel.Result);
        }

        [Fact]
        public async Task AtNoPresentUserValueAddingShouldReturnFalse()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new ApplicationDbContext(builder.Options);

            var userRepository = new EfDeletableEntityRepository<FamilyKitchenUser>(dbContext);
            var favoritesRepository = new EfRepository<FamilyKitchenUserFavoriteProduct>(dbContext);
            var productRepository = new EfDeletableEntityRepository<ShopProduct>(dbContext);

            var favoritesService = new FavoriteProductService(userRepository, favoritesRepository, productRepository);

            await userRepository.AddAsync(new FamilyKitchenUser
            {
                UserName = "qsen.peichev@gmail.com",
                Email = "qsen.peichev@gmail.com",
                ClientCard = new ClientCard(),
                ShoppingCart = new ShoppingCart(),
            });
            await userRepository.SaveChangesAsync();

            await productRepository.AddAsync(new ShopProduct { Id = 2, Name = "boza", Price = 2m, Discount = 0, });
            await productRepository.SaveChangesAsync();

            var actAdd = await favoritesService.Add(2, "chavdar.peichev@gmail.com");

            Assert.False(actAdd);
        }

        [Fact]
        public async Task AtNoPresentProductValueAddingShouldReturnFalse()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new ApplicationDbContext(builder.Options);

            var userRepository = new EfDeletableEntityRepository<FamilyKitchenUser>(dbContext);
            var favoritesRepository = new EfRepository<FamilyKitchenUserFavoriteProduct>(dbContext);
            var productRepository = new EfDeletableEntityRepository<ShopProduct>(dbContext);

            var favoritesService = new FavoriteProductService(userRepository, favoritesRepository, productRepository);

            await userRepository.AddAsync(new FamilyKitchenUser
            {
                UserName = "qsen.peichev@gmail.com",
                Email = "qsen.peichev@gmail.com",
                ClientCard = new ClientCard(),
                ShoppingCart = new ShoppingCart(),
            });
            await userRepository.SaveChangesAsync();

            await productRepository.AddAsync(new ShopProduct { Id = 2, Name = "boza", Price = 2m, Discount = 0, });
            await productRepository.SaveChangesAsync();

            var actAdd = await favoritesService.Add(22, "qsen.peichev@gmail.com");

            Assert.False(actAdd);
        }

        [Fact]
        public async Task AtNoPresentUserValueDeletingShouldReturnFalse()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new ApplicationDbContext(builder.Options);

            var userRepository = new EfDeletableEntityRepository<FamilyKitchenUser>(dbContext);
            var favoritesRepository = new EfRepository<FamilyKitchenUserFavoriteProduct>(dbContext);
            var productRepository = new EfDeletableEntityRepository<ShopProduct>(dbContext);

            var favoritesService = new FavoriteProductService(userRepository, favoritesRepository, productRepository);

            await userRepository.AddAsync(new FamilyKitchenUser
            {
                UserName = "qsen.peichev@gmail.com",
                Email = "qsen.peichev@gmail.com",
                ClientCard = new ClientCard(),
                ShoppingCart = new ShoppingCart(),
            });
            await userRepository.SaveChangesAsync();

            await productRepository.AddAsync(new ShopProduct { Id = 2, Name = "boza", Price = 2m, Discount = 0, });
            await productRepository.SaveChangesAsync();

            var actAdd = await favoritesService.Add(2, "chavdar.peichev@gmail.com");

            Assert.False(actAdd);
        }

        [Fact]
        public async Task AtNoPresentProductValueDeletingShouldReturnFalse()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new ApplicationDbContext(builder.Options);

            var userRepository = new EfDeletableEntityRepository<FamilyKitchenUser>(dbContext);
            var favoritesRepository = new EfRepository<FamilyKitchenUserFavoriteProduct>(dbContext);
            var productRepository = new EfDeletableEntityRepository<ShopProduct>(dbContext);

            var favoritesService = new FavoriteProductService(userRepository, favoritesRepository, productRepository);

            await userRepository.AddAsync(new FamilyKitchenUser
            {
                UserName = "qsen.peichev@gmail.com",
                Email = "qsen.peichev@gmail.com",
                ClientCard = new ClientCard(),
                ShoppingCart = new ShoppingCart(),
            });
            await userRepository.SaveChangesAsync();

            await productRepository.AddAsync(new ShopProduct { Id = 2, Name = "boza", Price = 2m, Discount = 0, });
            await productRepository.SaveChangesAsync();

            var actAdd = await favoritesService.Delete(22, "qsen.peichev@gmail.com");

            Assert.False(actAdd);
        }
    }
}
