namespace FamilyKitchen.Web.Controllers
{
    using System.Net;
    using System.Net.Mail;
    using FamilyKitchen.Common;
    using FamilyKitchen.Web.ViewModels.MailSender;
    using Microsoft.AspNetCore.Mvc;

    public class MailSenderController : BaseController
    {
        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contact(ContactInputModel input)
        {
            this.SendEmail(input.Name, input.Email, GlobalConstants.EmailMessageAnswer);

            return this.Redirect(nameof(this.Contact));
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
