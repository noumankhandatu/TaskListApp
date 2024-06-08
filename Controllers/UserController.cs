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
        public IActionResult Register(User user)
        {
            // Validate user input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add user to database
            var registeredUser = _userService.Register(user);

            return Ok(registeredUser);
        }
    }
}
