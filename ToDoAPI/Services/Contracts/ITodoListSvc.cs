using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Models;
using ToDoAPI.Models.Requests;
using ToDoAPI.Models.Response;

namespace ToDoAPI.Services.Contracts
{
    public interface ITodoListSvc
    {
        IEnumerable<ToDoListResponse> GetToDoLists();
        ToDoListResponse GetToDoList(int id);
        ToDoListResponse CreateToDoList(string value);
        ToDoListResponse UpdateToDoList(int id, string value);
        void DeleteToDoList(int id);

        TaskResponse CreateTask(int listid, CreateTaskRequest value);
        TaskResponse UpdateTask(int listid, UpdateTaskRequest value);
        void DeleteTask(int listid, int id);
        IEnumerable<TaskResponse> GetTasks(int listid, int? priority, bool? completed, string orderby);
        TaskResponse GetTask(int listid, int id);
    }
}
