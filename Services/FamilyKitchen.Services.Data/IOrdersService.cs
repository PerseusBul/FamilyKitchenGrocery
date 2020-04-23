namespace FamilyKitchen.Services.Data
{
    using System.Threading.Tasks;

    using FamilyKitchen.Web.ViewModels.Orders;

    public interface IOrdersService
    {
        Task<bool> CreateOrder(string username, OrderProfileInputModel profile, string paymentMethod);
    }
}
