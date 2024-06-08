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

        public User GetUserByUsername(string username)
        {
            return _users.Find(u => u.Username == username).SingleOrDefault();
        }
        public User Authenticate(string username, string password)
        {
            var user = _users.Find(u => u.Username == username && u.Password == password).SingleOrDefault();
            return user;
        }

        // // Assign a task to a user by task ID and user ID
        // public void AssignTaskToUser(string taskId, string userId)
        // {
        //     var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        //     var update = Builders<User>.Update.Push(u => u.AssignedTasks, taskId);
        //     _users.UpdateOne(filter, update);
        // }

        // Get all users
        public List<User> GetAllUsers()
        {
            return _users.Find(user => true).ToList();
        }
    }
}
