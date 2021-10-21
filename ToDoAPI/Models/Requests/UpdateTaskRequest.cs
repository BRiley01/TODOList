using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models.Requests
{
    public class UpdateTaskRequest
    {
        public string Title { get; set; }
        public int Priority { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public bool Completed { get; set; }
        public int ID { get; set; }

        public ToDoTask ToTask()
        {
            return new ToDoTask
            {
                Title = this.Title,
                Priority = this.Priority,
                DueDate = this.DueDate,
                Completed = this.Completed,
                ID = this.ID
            };
        }
    }
}
