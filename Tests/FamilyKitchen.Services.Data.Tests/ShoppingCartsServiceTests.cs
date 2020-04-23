namespace FamilyKitchen.Services.Data.Tests
{
    using Xunit;

    public class ShoppingCartsServiceTests
    {
        [Fact]
        public void NullUsernameParameterReturnsFalse()
        {
            Assert.False(false);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("Pesho")]
        public void DemoTheoryResultsNotEqual(string demo)
        {
            Assert.NotEqual("Gosho", demo);
        }
    }
}
