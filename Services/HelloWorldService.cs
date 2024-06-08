// Services/HelloWorldService.cs
using MongoDB.Driver;
using TaskListApp.Models;
using Microsoft.Extensions.Options;

namespace TaskListApp.Services
{
    public class HelloWorldService
    {
        private readonly IMongoCollection<HelloWorldItem> _helloWorldItems;

        public HelloWorldService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var client = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _helloWorldItems = database.GetCollection<HelloWorldItem>("HelloWorldItems");
        }

        public async Task<HelloWorldItem> CreateAsync(HelloWorldItem item)
        {
            await _helloWorldItems.InsertOneAsync(item);
            return item;
        }
    }
}
