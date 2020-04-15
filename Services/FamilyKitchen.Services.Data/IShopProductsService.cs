namespace FamilyKitchen.Services.Data
{
    using FamilyKitchen.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShopProductsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllProducts<T>();

        IEnumerable<T> GetAllMeals<T>();

        T GetProductById<T>(int id);

        Task Update(ShopProduct product);

        Task Delete(ShopProduct product);

        Task Add(ShopProduct product);
    }
}
