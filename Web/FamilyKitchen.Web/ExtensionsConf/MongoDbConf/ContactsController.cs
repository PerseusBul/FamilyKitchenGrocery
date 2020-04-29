namespace FamilyKitchen.Web.ExtensionsConf.MongoDbConf
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsService contactsService;

        public ContactsController(ContactsService contactsService)
        {
            this.contactsService = contactsService;
        }

        [HttpGet]
        public ActionResult<List<Contact>> Get() =>
            this.contactsService.Get();

        [HttpGet("{id:length(24)}", Name = "GetContact")]
        public ActionResult<Contact> Get(string id)
        {
            var contact = this.contactsService.Get(id);

            if (contact == null)
            {
                return this.NotFound();
            }

            return contact;
        }

        [HttpPost]
        public ActionResult<Contact> Create(Contact contact)
        {
            this.contactsService.Create(contact);

            return this.CreatedAtRoute("GetContact", new { id = contact.Id.ToString() }, contact);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Contact contactIn)
        {
            var contact = this.contactsService.Get(id);

            if (contact == null)
            {
                return this.NotFound();
            }

            this.contactsService.Update(id, contactIn);

            return this.NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var contact = this.contactsService.Get(id);

            if (contact == null)
            {
                return this.NotFound();
            }

            this.contactsService.Remove(contact.Id);

            return this.NoContent();
        }
    }
}
