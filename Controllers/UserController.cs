using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Register(User registrationModel) // Update parameter type
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User // Creating a User object from registration model
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
    }
}
