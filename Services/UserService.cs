using MongoDB.Driver;
using TaskListApp.Models;
using System.Collections.Generic;

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

    
        // Get all users
        public List<User> GetAllUsers()
        {
            return _users.Find(user => true).ToList();
        }
    }
}
