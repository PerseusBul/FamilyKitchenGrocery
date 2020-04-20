using FamilyKitchen.Data.Common.Repositories;
using FamilyKitchen.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyKitchen.Services.Data
{
    public class FamilyKitchenUsersService : IFamilyKitchenUsersService
    {
        private IDeletableEntityRepository<FamilyKitchenUser> userRepository;

        public FamilyKitchenUsersService(IDeletableEntityRepository<FamilyKitchenUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public FamilyKitchenUser GetUserByName(string username)
        {
            var user = this.userRepository.All().Where(u => u.UserName == username).FirstOrDefault();

            return user;
        }

        public async Task GiveClientCartsAsync()
        {
            var users = this.userRepository.All().Where(u => u.ClientCard == null).ToList();

            if (users.Count() > 0)
            {
                foreach (var user in users)
                {
                    user.ClientCard = new ClientCard();
                    this.userRepository.Update(user);
                }

                await this.userRepository.SaveChangesAsync();
            }
        }
    }
}
