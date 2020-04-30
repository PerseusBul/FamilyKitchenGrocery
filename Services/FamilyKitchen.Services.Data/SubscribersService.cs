namespace FamilyKitchen.Services.Data
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using FamilyKitchen.Common;
    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;

    public class SubscribersService : ISubscribersService
    {
        private readonly IDeletableEntityRepository<Subscriber> subscribersRepository;

        public SubscribersService(IDeletableEntityRepository<Subscriber> subscribersRepository)
        {
            this.subscribersRepository = subscribersRepository;
        }

        public async Task<bool> SubscribeEmail(string username, string subscriber)
        {
            var subscriberEntity = new Subscriber()
            {
                Email = subscriber,
                Username = username,
                SubscriptionTime = DateTime.UtcNow,
            };

            await this.subscribersRepository.AddAsync(subscriberEntity);
            await this.subscribersRepository.SaveChangesAsync();

            return true;
        }

        public string SendEmail(string name, string email, string message)
        {
            try
            {
                var credentials = new NetworkCredential(GlobalConstants.FamilyKitchenEmail, GlobalConstants.FKEmailPassword);

                var mail = new MailMessage()
                {
                    From = new MailAddress(GlobalConstants.FamilyKitchenEmail),
                    Subject = GlobalConstants.EmailSubject,
                    Body = message,
                };
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(email));

                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = GlobalConstants.GmailHost,
                    EnableSsl = true,
                    Credentials = credentials,
                };
                client.Send(mail);
                return GlobalConstants.EmailGreeting;
            }
            catch (System.Exception e)
            {
                return e.Message;
            }
        }
    }
}
