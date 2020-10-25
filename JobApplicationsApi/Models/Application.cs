using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JobApplicationsApi.Models
{
    public class Application
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();

        public string UserId { get; set; }//FK
    }
}
