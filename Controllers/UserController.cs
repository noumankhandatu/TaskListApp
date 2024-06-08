using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskListApp.Models;
using TaskListApp.Services;

namespace TaskListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

      [HttpPost("register")]
public IActionResult Register(User registrationModel)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Check if the username already exists
    var existingUser = _userService.GetUserByUsername(registrationModel.Username);
    if (existingUser != null)
    {
        // User with the same username already exists, return a conflict response
        return Conflict("Username is already taken.");
    }

    var user = new User
    {
        Username = registrationModel.Username,
        Password = registrationModel.Password,
        Role = registrationModel.Role
    };

    var registeredUser = _userService.Register(user);
    return CreatedAtAction(nameof(Register), new { id = registeredUser.Id }, registeredUser);
}

        [HttpPost("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userService.Authenticate(loginModel.Username, loginModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
