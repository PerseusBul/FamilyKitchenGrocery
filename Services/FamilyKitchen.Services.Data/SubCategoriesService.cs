namespace FamilyKitchen.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;

    public class SubCategoriesService : ISubCategoriesService
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoriesRepository;

        public SubCategoriesService(IDeletableEntityRepository<SubCategory> subCategoriesRepository)
        {
            this.subCategoriesRepository = subCategoriesRepository;
        }
    }
}
