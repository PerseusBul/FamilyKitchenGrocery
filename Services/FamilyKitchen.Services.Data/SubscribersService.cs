namespace FamilyKitchen.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;

    public class SubscribersService : ISubscribersService
    {
        private readonly IDeletableEntityRepository<Subscriber> subscribersRepository;

        public SubscribersService(IDeletableEntityRepository<Subscriber> subscribersRepository)
        {
            this.subscribersRepository = subscribersRepository;
        }

        public async Task<bool> SubscribeEmail(string username, string subscriber)
        {
            var subscriberEntity = new Subscriber()
            {
                Email = subscriber,
                Username = username,
                SubscriptionTime = DateTime.UtcNow,
            };

            await this.subscribersRepository.AddAsync(subscriberEntity);
            await this.subscribersRepository.SaveChangesAsync();

            return true;
        }
    }
}
