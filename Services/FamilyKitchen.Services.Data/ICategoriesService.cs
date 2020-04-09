using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyKitchen.Services.Data
{
    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllProducts<T>();

        IEnumerable<T> GetAllMealCategories<T>();

        T GetByName<T>(string name);
    }
}
