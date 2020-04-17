namespace FamilyKitchen.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Web.ViewModels.ShoppingCarts;

    public interface IShoppingCartsService
    {
        Task<bool> AddProduct(int id, string username);

        ShoppingCartShopProduct GetProduct(int id, string username);

        Task<bool> DeleteProduct(int id, string username);

        Task EditProduct(int id, string username, int quantity);

        IEnumerable<CartProductViewModel> GetAllProducts(string username);

        bool DeleteAll(string username);
    }
}
