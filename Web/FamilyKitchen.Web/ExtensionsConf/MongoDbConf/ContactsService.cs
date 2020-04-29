using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyKitchen.Web.ExtensionsConf.MongoDbConf
{
    public class ContactsService
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Contact> contacts;

        public ContactsService(IMongoDbDatabaseSettings databaseSettings)
        {
            this.client = new MongoClient(databaseSettings.ConnectionString);
            this.database = this.client.GetDatabase(databaseSettings.DatabaseName);
            this.contacts = this.database.GetCollection<Contact>(databaseSettings.CollectionName);
        }

        public List<Contact> Get() =>
            this.contacts.Find(contact => true).ToList();

        public Contact Get(string id) =>
            this.contacts.Find<Contact>(contact => contact.Id == id).FirstOrDefault();

        public Contact Create(Contact contact)
        {
            this.contacts.InsertOne(contact);
            return contact;
        }

        public void Update(string id, Contact contactIn) =>
            this.contacts.ReplaceOne(contact => contact.Id == id, contactIn);

        public void Remove(Contact contactIn) =>
            this.contacts.DeleteOne(contact => contact.Id == contactIn.Email);

        public void Remove(string id) =>
            this.contacts.DeleteOne(contact => contact.Id == id);
    }
}
