using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models.Requests
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public int Priority { get; set; }
        public DateTimeOffset DueDate { get; set; }
    }
}
