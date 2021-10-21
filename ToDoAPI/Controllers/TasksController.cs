using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Exceptions;
using ToDoAPI.Models.Requests;
using ToDoAPI.Models.Response;
using ToDoAPI.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoAPI.Controllers
{
    [Route("api/v1/todo-lists/{listid}/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<ToDoListsController> _logger;
        private readonly ITodoListSvc _toDoSvc;

        public TasksController(ILogger<ToDoListsController> logger, ITodoListSvc toDoSvc)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _toDoSvc = toDoSvc ?? throw new ArgumentNullException(nameof(toDoSvc));
        }

        // GET: api/v1/<TasksController>
        [HttpGet]
        public ActionResult<IEnumerable<TaskResponse>> Get(int listid, int? priority, bool? completed, string orderby)
        {
            try
            {
                return Ok(_toDoSvc.GetTasks(listid, priority, completed, orderby));
            }
            catch (ResourceNotFoundException)
            {
                return BadRequest($"To do list {listid} not found");
            }
        }

        // GET api/v1/todo-lists/{listid}/tasks/5
        [HttpGet("{id}")]
        public ActionResult<TaskResponse> Get(int listid, int id)
        {
            try
            {
                return _toDoSvc.GetTask(listid, id);
            }
            catch (ResourceNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/v1/todo-lists/{listid}/tasks
        [HttpPost]
        public ActionResult<TaskResponse> Post(int listid , [FromBody] CreateTaskRequest value)
        {
            try
            {
                return _toDoSvc.CreateTask(listid, value);
            }
            catch(ResourceNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/v1/todo-lists/{listid}/tasks/5
        [HttpPut("{id}")]
        public ActionResult<TaskResponse> Put(int listid, int id, [FromBody] UpdateTaskRequest value)
        {
            try
            {
                return _toDoSvc.UpdateTask(listid, value);
            }
            catch (ResourceNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/v1/todo-lists/{listid}/tasks/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int listid, int id)
        {
            try
            {
                _toDoSvc.DeleteTask(listid, id);
                return Ok();
            }
            catch (ResourceNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
