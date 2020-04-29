namespace FamilyKitchen.Web.Controllers
{
    using System.Net;
    using System.Net.Mail;
    using FamilyKitchen.Common;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.MailSender;
    using Microsoft.AspNetCore.Mvc;

    public class MailSenderController : BaseController
    {
        private readonly ISubscribersService subscribersService;

        public MailSenderController(ISubscribersService subscribersService)
        {
            this.subscribersService = subscribersService;
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contact(ContactInputModel input)
        {
            this.subscribersService.SendEmail(input.Name, input.Email, GlobalConstants.EmailMessageAnswer);

            return this.Redirect(nameof(this.Contact));
        }
    }
}
