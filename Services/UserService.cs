using MongoDB.Driver;
using TaskListApp.Models;

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
            _users.InsertOne(user);
            return user;
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.Find(u => u.Username == username && u.Password == password).SingleOrDefault();
            return user;
        }
    }
}
