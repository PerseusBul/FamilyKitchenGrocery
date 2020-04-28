namespace FamilyKitchen.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;

    public interface IShopProductsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllProducts<T>();

        IEnumerable<T> GetAllMeals<T>();

        T GetProductById<T>(int id);

        T GetProductByName<T>(string name);

        Task Update(ShopProduct product);

        Task Delete(ShopProduct product);

        Task<bool> Add(ShopProduct product);

        IEnumerable<ShopProduct> ProduceNewKitchenProducts(IQueryable<Recipe> meals);
    }
}
