// Models/HelloWorldItem.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskListApp.Models
{
    public class HelloWorldItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public bool Value { get; set; }
    }
}
