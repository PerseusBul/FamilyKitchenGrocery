namespace FamilyKitchen.Services.Data
{
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Web.ViewModels.SubCategories;

    public class SubCategoriesService : ISubCategoriesService
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoriesRepository;
        private readonly IMapper mapper;

        public SubCategoriesService(IDeletableEntityRepository<SubCategory> subCategoriesRepository,
                                    IMapper mapper)
        {
            this.subCategoriesRepository = subCategoriesRepository;
            this.mapper = mapper;
        }

        public int GetCategoryId(int id)
        {
            int categoryId = this.subCategoriesRepository
                .All()
                .Where(x => x.Id == id)
                .Select(x => x.CategoryId)
                .FirstOrDefault();

            return categoryId;
        }

        public SubCategoryViewModel GetSubcategoryById(int id)
        {
            var subCategory = this.subCategoriesRepository
                .All()
                .Where(x => x.Id == id)
                .ProjectTo<SubCategoryViewModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

            return subCategory;
        }
    }
}
