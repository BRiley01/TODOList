using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoAPI.DAL.Contracts;
using ToDoAPI.Exceptions;
using ToDoAPI.Models;
using ToDoAPI.Models.Requests;
using ToDoAPI.Models.Response;
using ToDoAPI.Services.Contracts;

namespace ToDoAPI.Services
{
    /// <summary>
    /// Service to interface with Task/todo repos.  This allows a layer for business logic prior to calling data layer
    /// </summary>

    public class TodoListSvc : ITodoListSvc
    {
        private readonly IToDoDAL _DAL; //Data access layer
        private readonly ILogger<TodoListSvc> _logger;

        public TodoListSvc(ILogger<TodoListSvc> logger, IToDoDAL DAL)
        {
            _DAL = DAL ?? throw new ArgumentNullException(nameof(DAL));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates new todo list.  If already exists, throws: ResourceAlreadyExistsException

        #region TODO List
        /// </summary>
        /// <param name="value">Name of new todo list</param>
        public ToDoListResponse CreateToDoList(string value)
        {
            _logger.LogTrace($"Creating todo list {value}");
            
            //Example business logic that may live at this layer
            if ((value ?? "").Length < 3)
                throw new Exception("to do list name must be 3 or more characters");
            var list = _DAL.SaveToDoList(new ToDoList { Name = value });
            return new ToDoListResponse(list);
        }

        public void DeleteToDoList(int id)
        {
            if (_DAL.GetToDoList(id) == null)
                throw new ResourceNotFoundException();
            _DAL.DeleteToDoList(id);
        }

        public ToDoListResponse GetToDoList(int id)
        {
            return new ToDoListResponse(_DAL.GetToDoList(id));
        }

        public IEnumerable<ToDoListResponse> GetToDoLists()
        {
            return _DAL.GetToDoLists().Select(l => new ToDoListResponse(l));
        }

        public ToDoListResponse UpdateToDoList(int id, string value)
        {
            var list = _DAL.GetToDoList(id);
            if (list == null) throw new ResourceNotFoundException();
            list.Name = value;
            _DAL.SaveToDoList(list);
            return new ToDoListResponse(list);
        }
        #endregion

        #region Tasks

        protected ToDoList GetListOrThrow(int listid)
        {
            var list = _DAL.GetToDoList(listid);
            if (list == null) throw new ResourceNotFoundException($"List {listid} not found");
            return list;
        }

        public TaskResponse CreateTask(int listid, CreateTaskRequest taskReq)
        {
            var list = GetListOrThrow(listid);
            var newID = list.NextTaskID;
            var newTask = new ToDoTask
            {
                Title = taskReq.Title,
                Priority = taskReq.Priority,
                DueDate = taskReq.DueDate,
                Completed = false,
                ID = newID
            };
            list.Tasks.Add(newID, newTask);
            _DAL.SaveToDoList(list);
            return new TaskResponse(newTask);
        }
                
        public void DeleteTask(int listid, int id)
        {
            var list = GetListOrThrow(listid);
            if (!list.Tasks.ContainsKey(id))
                throw new ResourceNotFoundException($"List {listid} task id {id} not found");
            list.Tasks.Remove(id);
            _DAL.SaveToDoList(list);
        }

        public TaskResponse GetTask(int listid, int id)
        {
            var list = GetListOrThrow(listid);
            if (!list.Tasks.ContainsKey(id))
                return null;
            return new TaskResponse(list.Tasks[id]);
        }

        public IEnumerable<TaskResponse> GetTasks(int listid, int? priority, bool? completed, string orderby)
        {
            var list = GetListOrThrow(listid);
            var tasks = list.Tasks.Values.AsEnumerable();
            if (priority.HasValue)
                tasks = tasks.Where(t => t.Priority == priority);
            if (completed.HasValue)
                tasks = tasks.Where(t => t.Completed == completed);
            if(!string.IsNullOrWhiteSpace(orderby))
            {
                switch(orderby)
                {
                    case "due-date":
                        tasks = tasks.OrderBy(t => t.DueDate);
                        break;
                    case "priority":
                        tasks = tasks.OrderBy(t => t.Priority);
                        break;
                    default:
                        throw new Exception("Unknown orderby - accepted values: due-date, priority");
                }
            }
            return tasks.Select(t => new TaskResponse(t));
        }

        public TaskResponse UpdateTask(int listid, UpdateTaskRequest value)
        {
            var list = GetListOrThrow(listid);
            if (!list.Tasks.ContainsKey(value.ID))
                throw new ResourceNotFoundException($"List {listid} task id {value.ID} not found");
            var newTask = value.ToTask();
            list.Tasks[value.ID] = newTask;
            return new TaskResponse(newTask);
        }
        #endregion
    }
}
