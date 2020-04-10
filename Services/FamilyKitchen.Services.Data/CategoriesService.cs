namespace FamilyKitchen.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Mapping;
    using FamilyKitchen.Web.ViewModels.SubCategories;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoriesRepository;
        private readonly IMapper mapper;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository, 
                                 IDeletableEntityRepository<SubCategory> subCategoriesRepository,
                                 IMapper mapper)
        {
            this.categoriesRepository = categoriesRepository;
            this.subCategoriesRepository = subCategoriesRepository;
            this.mapper = mapper;
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
                .Where(cat => cat.Id == 14 || cat.Id == 15);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllProductCategories<T>()
        {
            IQueryable<Category> query =
                this.categoriesRepository.All().Where(x => x.Id <= 12); // .OrderBy(x => x.Name)

            return query.To<T>().ToList();
        }

        public IEnumerable<SubCategoryViewModel> GetSubCategoriesByCategoryId(int id)
        {
            var subCategories = this.subCategoriesRepository
                .All()
                .Where(x => x.CategoryId == id)
                .ProjectTo<SubCategoryViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return subCategories;
        }

        public T GetByName<T>(string name)
        {
            var category = this.categoriesRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();
            return category;
        }
    }
}
