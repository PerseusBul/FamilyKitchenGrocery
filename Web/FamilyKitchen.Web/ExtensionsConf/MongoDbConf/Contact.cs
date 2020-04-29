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

        [BsonElement("email")]
        [JsonProperty("email")]
        public string Email { get; set; }

        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [BsonElement("subject")]
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [BsonElement("message")]
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
