namespace FamilyKitchen.Web.ExtensionsConf.MongoDbConf
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using Newtonsoft.Json;

    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Email")]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [BsonElement("Subject")]
        [JsonProperty("Subject")]
        public string Subject { get; set; }

        [BsonElement("Message")]
        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
