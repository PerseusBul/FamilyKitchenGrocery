namespace FamilyKitchen.Web.Tests
{
    using FamilyKitchen.Web.ViewModels.ShopProducts;
    using Xunit;

    public class ShopProductViewModelTests
    {
        [Theory]
        [InlineData(3, 0)]
        [InlineData(0, 0)]
        [InlineData(0, 3)]
        [InlineData(20, 20)]
        public void SalePriceShouldBeNotEqual(decimal price, decimal discount)
        {
            var model = new ShopProductViewModel
            {
                Id = 3,
                Price = price,
                Discount = discount,
                Name = "Boza",
            };

            Assert.NotEqual(4m, model.SalePrice);
        }

        [Fact]
        public void SalePriceShouldBeEqual()
        {
            var model = new ShopProductViewModel
            {
                Id = 10,
                Price = 10,
                Discount = 10,
                Name = "Boza",
            };

            Assert.Equal(9.00m, model.SalePrice);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(-3)]
        [InlineData(0)]
        [InlineData(-4)]
        [InlineData(0.4)]
        [InlineData(40)]
        public void ImageUrlShouldBeNotEqual(int id)
        {
            var model = new ShopProductViewModel
            {
                Id = id,
                Price = 10,
                Discount = 10,
                Name = "Boza",
            };

            Assert.NotEqual($"/images/product_4.jpg", model.ImageUrl);
        }

        [Fact]
        public void ImageUrlShouldBeEqual()
        {
            var model = new ShopProductViewModel
            {
                Id = 4,
                Price = 10,
                Discount = 10,
                Name = "Boza",
            };

            Assert.Equal($"/images/product_4.jpg", model.ImageUrl);
        }
    }
}
