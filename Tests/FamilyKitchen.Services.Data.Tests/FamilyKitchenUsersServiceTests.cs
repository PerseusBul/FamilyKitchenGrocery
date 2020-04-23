namespace FamilyKitchen.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using Moq;
    using Xunit;

    public class FamilyKitchenUsersServiceTests
    {
        [Fact]
        public void MethodShouldWorkProperlyReturnsTrue()
        {
            var repository = new Mock<IDeletableEntityRepository<FamilyKitchenUser>>();
            repository.Setup(x => x.All()).Returns(this.TestDataUserRepository().AsQueryable());

            IFamilyKitchenUsersService usersService = new FamilyKitchenUsersService(repository.Object);
            FamilyKitchenUser user = usersService.GetUserByName("Test@gmail.com");

            Assert.True(user.Email == "Test@gmail.com");
            Assert.False(user.Id == "d3d03427-f9fa-43c2-b88b-5695decf3326");
        }

        public IEnumerable<FamilyKitchenUser> TestDataUserRepository()
        {
            var users = new List<FamilyKitchenUser>()
            {
                new FamilyKitchenUser
                {
                    Id = "efe2b426-cf28-450f-bb66-e8132388213e",
                    UserName = "Test@gmail.com",
                    Email = "Test@gmail.com",
                },
                new FamilyKitchenUser
                {
                    Id = "d3d03427-f9fa-43c2-b88b-5695decf3326",
                    UserName = "Test@claim.bg",
                    Email = "Test@claim.bg",
                },
            };

            return users;
        }
    }
}
