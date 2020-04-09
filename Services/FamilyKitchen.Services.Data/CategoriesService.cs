using FamilyKitchen.Data.Common.Repositories;
using FamilyKitchen.Data.Models;
using FamilyKitchen.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyKitchen.Services.Data
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query =
                this.categoriesRepository.All(); // .OrderBy(x => x.Name)
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllMealCategories<T>()
        {
            IQueryable<Category> query =
                this.categoriesRepository.All()
                .Where(cat => cat.Id == 14 || cat.Id == 15); // .OrderBy(x => x.Name)

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllProducts<T>()
        {
            IQueryable<Category> query =
                this.categoriesRepository.All().Where(x => x.Id <= 12); // .OrderBy(x => x.Name)

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var category = this.categoriesRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();
            return category;
        }
    }
}
