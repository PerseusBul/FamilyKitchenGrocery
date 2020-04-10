using FamilyKitchen.Data.Common.Repositories;
using FamilyKitchen.Data.Models;
using FamilyKitchen.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyKitchen.Services.Data
{
    public class FoodResourcesService : IFoodResourcesService
    {
        private readonly IDeletableEntityRepository<FoodResource> resourcesRepository;

        public FoodResourcesService(IDeletableEntityRepository<FoodResource> resourcesRepository)
        {
            this.resourcesRepository = resourcesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<FoodResource> query =
                this.resourcesRepository.All();

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
