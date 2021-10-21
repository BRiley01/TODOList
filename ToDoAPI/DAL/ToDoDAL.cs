using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.DAL.Contracts;
using ToDoAPI.Exceptions;
using ToDoAPI.Models;

namespace ToDoAPI.DAL
{
    public class ToDoDAL : IToDoDAL
    {
        private int MaxID = 0;
        private ConcurrentDictionary<int, ToDoList> _storage = new ConcurrentDictionary<int, ToDoList>();
        public void DeleteToDoList(int id)
        {
            ToDoList lst;
            if (!_storage.ContainsKey(id)) throw new ResourceNotFoundException();
            _storage.Remove(id, out lst);
        }

        public ToDoList GetToDoList(int id)
        {
            ToDoList lst;
            if(!_storage.TryGetValue(id, out lst)) throw new ResourceNotFoundException();
            return lst;
        }

        public IEnumerable<ToDoList> GetToDoLists()
        {
            return _storage.Values;
        }

        public ToDoList SaveToDoList(ToDoList list)
        {
            if(!list.ID.HasValue)
            {
                lock (this)
                {
                    list.ID = ++MaxID;
                }
            }
            _storage.AddOrUpdate(list.ID.Value, list, (i,l1) => list);
            return list;
        }
    }
}
