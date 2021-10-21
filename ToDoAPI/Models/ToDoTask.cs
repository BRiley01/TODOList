using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models
{
    public class ToDoTask
    {
        public string Title { get; internal set; }
        public int Priority { get; internal set; }
        public DateTimeOffset DueDate { get; internal set; }
        public bool Completed { get; internal set; }
        public int ID { get; internal set; }
    }
}
