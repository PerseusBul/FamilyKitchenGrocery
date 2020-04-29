namespace FamilyKitchen.Services.Data
{
    using System.Threading.Tasks;

    public interface IClientCardsService
    {
        Task ApplyVoucher(string username, string numberCode);

        Task ApplyDeliveryPrice(string username, decimal deliveryPrice);

        bool IsValid(string numberCode);

        decimal DeliveryPriceCalculator(string city, string destination);

        Task<string> VoucherGenerator();
    }
}
