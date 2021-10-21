using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models.Response
{
    public class TaskResponse
    {
        public TaskResponse()
        {

        }

        public TaskResponse(ToDoTask newTask)
        {
            Title = newTask.Title;
            Priority = newTask.Priority;
            DueDate = newTask.DueDate;
            Completed = newTask.Completed;
            ID = newTask.ID;
        }

        public string Title { get; }
        public int Priority { get; }
        public DateTimeOffset DueDate { get; }
        public bool Completed { get; }
        public int ID { get; }
    }
}
