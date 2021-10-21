using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models
{
    public class ToDoList
    {
        private int _nextTaskID = 0;

        public ToDoList()
        {
            Tasks = new Dictionary<int, ToDoTask>();
        }

        public int? ID { get; internal set; }
        public string Name { get; internal set; }      

        public int NextTaskID { get { return ++_nextTaskID; } }
        
        public Dictionary<int, ToDoTask> Tasks { get; }
    }
}
