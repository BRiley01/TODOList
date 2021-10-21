using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models
{
    public class ToDoListResponse
    {
        public ToDoListResponse()
        {

        }

        public ToDoListResponse(ToDoList toDoList)
        {
            ID = toDoList.ID??-1;
            Name = toDoList.Name;
        }

        public int ID { get; internal set; }
        public string Name { get; internal set; }
    }
}
