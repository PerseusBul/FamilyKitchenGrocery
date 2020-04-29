namespace FamilyKitchen.Web.ExtensionsConf.MongoDbConf
{
    public class MongoDbDatabaseSettings : IMongoDbDatabaseSettings
    {
        public string CollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
