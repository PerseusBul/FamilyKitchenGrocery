namespace FamilyKitchen.Services.Data
{
    using System.Collections.Generic;

    public interface IShopProductsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllProducts<T>();

        IEnumerable<T> GetAllMeals<T>();

        T GetProductById<T>(int id);
    }
}
