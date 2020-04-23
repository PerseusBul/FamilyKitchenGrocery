namespace FamilyKitchen.Web.Tests
{
    using System.Collections.Generic;

    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.Controllers;
    using FamilyKitchen.Web.ViewModels.ShopProducts;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class FavoriteProductsControllerTests
    {
        [Fact]
        public void MethodListShouldWorkProperly()
        {
            var mockService = new Mock<IFavoriteProductService>();
            Assert.IsType<Mock<IFavoriteProductService>>(mockService);
            mockService.Setup(x => x.ListAll<ShopProductViewModel>("Gosho")).Returns(new List<ShopProductViewModel>()
            {
                new ShopProductViewModel { Id = 1, Name = "Boza", Price = 1.55m, Discount = 10, },
                new ShopProductViewModel { Id = 2, Name = "Limonada", Price = 1.05m, Discount = 5, },
            });

            var controller = new FavoriteProductsController(mockService.Object);
            Assert.IsType<FavoriteProductsController>(controller);
            var result = (List<ShopProductViewModel>)mockService.Object.ListAll<ShopProductViewModel>("Gosho");
            Assert.Equal(2, result.Count);

            mockService.Verify(x => x.ListAll<ShopProductViewModel>("Gosho"), Times.Once);
        }
    }
}
