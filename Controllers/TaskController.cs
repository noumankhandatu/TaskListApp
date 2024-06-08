using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MicrosoftWebApi.Models;
using System.Collections.Generic;
using TaskModel = MicrosoftWebApi.Models.Task; // Alias for your Task model

namespace MicrosoftWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMongoCollection<TaskModel> _tasks; // Use the alias TaskModel

        public TaskController(IMongoDatabase database)
        {
            _tasks = database.GetCollection<TaskModel>("tasks"); // Use the alias TaskModel
        }

        [HttpGet]
        public ActionResult<List<TaskModel>> Get() =>
            _tasks.Find(task => true).ToList();

        [HttpGet("{id:length(24)}", Name = "GetTask")]
        public ActionResult<TaskModel> Get(string id)
        {
            var task = _tasks.Find<TaskModel>(task => task.Id == id).FirstOrDefault();
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        [HttpPost]
        public ActionResult<TaskModel> Create(TaskModel task)
        {
            _tasks.InsertOne(task);
            return CreatedAtRoute("GetTask", new { id = task.Id.ToString() }, task);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, TaskModel taskIn)
        {
            var task = _tasks.Find<TaskModel>(task => task.Id == id).FirstOrDefault();
            if (task == null)
            {
                return NotFound();
            }
            _tasks.ReplaceOne(task => task.Id == id, taskIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var task = _tasks.Find<TaskModel>(task => task.Id == id).FirstOrDefault();
            if (task == null)
            {
                return NotFound();
            }
            _tasks.DeleteOne(task => task.Id == id);
            return NoContent();
        }
    }
}
