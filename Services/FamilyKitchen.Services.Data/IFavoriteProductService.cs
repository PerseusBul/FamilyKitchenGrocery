namespace FamilyKitchen.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFavoriteProductService
    {
        IEnumerable<T> ListAll<T>(string username);

        Task<bool> Add(int id, string username);

        Task<bool> Delete(int id, string username);
    }
}
