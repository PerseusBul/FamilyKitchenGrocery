namespace FamilyKitchen.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
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
