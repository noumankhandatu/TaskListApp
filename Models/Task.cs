using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MicrosoftWebApi.Models
{
    public class Task
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
