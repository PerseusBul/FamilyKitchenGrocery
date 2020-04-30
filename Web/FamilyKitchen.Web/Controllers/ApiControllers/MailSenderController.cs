namespace FamilyKitchen.Web.ApiControllers
{
    using System.Net;
    using System.Net.Mail;
    using AutoMapper;
    using FamilyKitchen.Common;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.Controllers;
    using FamilyKitchen.Web.ExtensionsConf.MongoDbConf;
    using FamilyKitchen.Web.ViewModels.MailSender;
    using Microsoft.AspNetCore.Mvc;

    public class MailSenderController : BaseController
    {
        private readonly ISubscribersService subscribersService;
        private readonly ContactsService contactsService;
        private readonly IMapper mapper;

        public MailSenderController(ISubscribersService subscribersService, ContactsService contactsService, IMapper mapper)
        {
            this.subscribersService = subscribersService;
            this.contactsService = contactsService;
            this.mapper = mapper;
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contact(ContactInputModel input)
        {
            this.subscribersService.SendEmail(input.Name, input.Email, GlobalConstants.EmailMessageAnswer);

            var contact = this.mapper.Map<Contact>(input);
            var response = this.contactsService.Create(contact);
            var result = this.contactsService.Get().FindAll(x => x.Email == "chavdar.peichev@gmail.com");

            return this.Redirect(nameof(this.Contact));
        }
    }
}
