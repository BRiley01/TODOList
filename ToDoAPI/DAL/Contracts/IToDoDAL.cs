using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Models;

namespace ToDoAPI.DAL.Contracts
{
    public interface IToDoDAL
    {
        ToDoList SaveToDoList(ToDoList list);
        ToDoList GetToDoList(int id);
        void DeleteToDoList(int id);
        IEnumerable<ToDoList> GetToDoLists();
    }
}
