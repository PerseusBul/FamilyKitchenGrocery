using FamilyKitchen.Web.ViewModels.SubCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyKitchen.Services.Data
{
    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllProductCategories<T>();

        IEnumerable<T> GetAllMealCategories<T>();

        IEnumerable<SubCategoryViewModel> GetSubCategoriesByCategoryId(int id);

        T GetByName<T>(string name);
    }
}
