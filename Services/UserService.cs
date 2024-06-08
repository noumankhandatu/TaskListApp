using TaskListApp.Models;
using MongoDB.Driver;

namespace TaskListApp.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("users");
        }

        public User Register(User user)
        {
            // Insert the user document into the MongoDB collection
            _users.InsertOne(user);
            return user;
        }
    }
}
