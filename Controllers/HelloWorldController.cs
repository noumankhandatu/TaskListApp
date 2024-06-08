// Controllers/HelloWorldController.cs
using Microsoft.AspNetCore.Mvc;
using TaskListApp.Models;
using TaskListApp.Services;
using System.Threading.Tasks;

namespace TaskListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        private readonly HelloWorldService _helloWorldService;

        public HelloWorldController(HelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }

        [HttpGet]
        public async Task<ActionResult<HelloWorldItem>> Get()
        {
            var newItem = new HelloWorldItem { Value = true };
            await _helloWorldService.CreateAsync(newItem);

            return Ok(newItem);
        }
    }
}
