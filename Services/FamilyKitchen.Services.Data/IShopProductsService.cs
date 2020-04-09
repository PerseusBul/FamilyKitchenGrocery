using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyKitchen.Services.Data
{
    public interface IShopProductsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllProducts<T>();

        IEnumerable<T> GetAllMeals<T>();

        T GetProductById<T>(int id);
    }
}
