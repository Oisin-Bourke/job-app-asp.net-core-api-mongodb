using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JobApplicationsApi.Models
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Description { get; set; }

        public string Body { get; set; }
    }
}
