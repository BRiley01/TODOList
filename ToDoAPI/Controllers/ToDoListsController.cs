using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Exceptions;
using ToDoAPI.Models;
using ToDoAPI.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoAPI.Controllers
{
    [Route("api/v1/todo-lists")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private readonly ILogger<ToDoListsController> _logger;
        private readonly ITodoListSvc _toDoSvc;

        public ToDoListsController(ILogger<ToDoListsController> logger, ITodoListSvc toDoSvc)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _toDoSvc = toDoSvc ?? throw new ArgumentNullException(nameof(toDoSvc));
        }

        /// <summary>
        /// Get a collection of all todo lists
        /// </summary>
        // GET api/v1/todo-lists
        [HttpGet]
        public IEnumerable<ToDoListResponse> Get()
        {
            return _toDoSvc.GetToDoLists();
        }

        /// <summary>
        /// Returns a single todo list
        /// </summary>
        /// <param name="id">id of to do list</param>
        // GET api/v1/todo-lists/<<id>>
        [HttpGet("{id}")]
        public ActionResult<ToDoListResponse> Get(int id)
        {
            var list = _toDoSvc.GetToDoList(id);
            if (list == null) return NotFound();
            return list;
        }

        /// <summary>
        /// Create a new todo list
        /// </summary>
        /// <param name="value">name of list</param>
        // POST api/v1/todo-lists
        [HttpPost]
        public ActionResult<ToDoListResponse> Post([FromBody] string value)
        {
            try
            {
                return _toDoSvc.CreateToDoList(value);
            }
            catch(ResourceAlreadyExistsException)
            {
                return BadRequest("To do list already exists");
            }

        }

        /// <summary>
        /// Change name of todo list
        /// </summary>
        // PUT api/v1/todo-lists/<<id>>
        [HttpPut("{id}")]
        public ActionResult<ToDoListResponse> Put(int id, [FromBody] string value)
        {
            try
            {
                return _toDoSvc.UpdateToDoList(id, value);
            }
            catch (ResourceNotFoundException)
            {
                return BadRequest($"To do list {id} not found");
            }
        }

        // DELETE api/<ToDoListsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _toDoSvc.DeleteToDoList(id);
                return Ok();
            }
            catch(ResourceNotFoundException)
            {
                return BadRequest($"To do list {id} not found");
            }
        }
    }
}
