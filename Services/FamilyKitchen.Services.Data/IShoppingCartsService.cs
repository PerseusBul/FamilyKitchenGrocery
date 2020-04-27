namespace FamilyKitchen.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Web.ViewModels.ShoppingCarts;

    public interface IShoppingCartsService
    {
        Task<bool> AddProduct(int id, string username, decimal quantity = 0);

        void AddSessionCart(List<CartProductViewModel> session, string username);

        ShoppingCartShopProduct GetProduct(int id, string username);

        Task<bool> DeleteProduct(int id, string username);

        Task EditProduct(int id, string username, bool upOrder);

        IEnumerable<CartProductViewModel> GetAllProducts(string username);

        Task<bool> DeleteAll(string username);

        Task<CartTotalViewModel> GetCartTotalParameters(string username);

        Task<bool> GetFamilyCart(FamilyKitchenUser moderator, FamilyKitchenUser member);

        Task<bool> ReturnFamilyCart(FamilyKitchenUser moderator, FamilyKitchenUser member);
    }
}
