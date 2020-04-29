namespace FamilyKitchen.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;

    public class ClientCardsService : IClientCardsService
    {
        private readonly IDeletableEntityRepository<ClientCard> cardRepository;
        private readonly IDeletableEntityRepository<Voucher> voucherRepository;
        private readonly IDeletableEntityRepository<FamilyKitchenUser> userRepository;

        public ClientCardsService(IDeletableEntityRepository<ClientCard> cardRepository,
                                IDeletableEntityRepository<Voucher> voucherRepository,
                                IDeletableEntityRepository<FamilyKitchenUser> userRepository)
        {
            this.cardRepository = cardRepository;
            this.voucherRepository = voucherRepository;
            this.userRepository = userRepository;
        }

        public async Task ApplyVoucher(string username, string numberCode)
        {
            var card = this.userRepository
                .All()
                .Where(x => x.UserName == username)
                .Select(y => y.ClientCard)
                .FirstOrDefault();

            var voucher = this.voucherRepository
                .All()
                .Where(x => x.NumberCode == numberCode)
                .FirstOrDefault();

            if (card == null || voucher == null)
            {
                return;
            }

            card.Voucher = voucher.Amount;
            this.cardRepository.Update(card);
            await this.cardRepository.SaveChangesAsync();

            this.voucherRepository.Delete(voucher);
            await this.voucherRepository.SaveChangesAsync();
        }

        public async Task ApplyDeliveryPrice(string username, decimal deliveryPrice)
        {
            var card = this.userRepository
                .All()
                .Where(x => x.UserName == username)
                .Select(y => y.ClientCard)
                .FirstOrDefault();

            if (card == null)
            {
                return;
            }

            card.DeliveryPrice = deliveryPrice;

            this.cardRepository.Update(card);
            await this.cardRepository.SaveChangesAsync();
        }

        public bool IsValid(string numberCode)
        {
            var isValid = this.voucherRepository
                .All()
                .Any(x => x.NumberCode == numberCode);

            return isValid;
        }

        public decimal DeliveryPriceCalculator(string city, string destination)
        {
            char startLetter = destination[0];
            int index = char.ToUpper(startLetter) - 64;
            decimal price = 10;

            if (city.ToLower() == "sofia")
            {
                if (index >= 1 && index <= 9)
                {
                    price = 4;
                }
                else if (index >= 10 && index <= 18)
                {
                    price = 7;
                }
            }
            else
            {
                price = 30;
            }

            return price;
        }

        public async Task<string> VoucherGenerator()
        {
            var voucherAmounts = new int[] { 10, 20, 30, 40, 50 };
            var numberCode = new Random();
            var indexAmount = new Random();

            var voucher = new Voucher
            {
                NumberCode = numberCode.Next(10000000, 99999999).ToString(),
                Amount = voucherAmounts[indexAmount.Next(0, 4)],
            };

            await this.voucherRepository.AddAsync(voucher);
            await this.voucherRepository.SaveChangesAsync();

            return voucher.NumberCode;
        }
    }
}
