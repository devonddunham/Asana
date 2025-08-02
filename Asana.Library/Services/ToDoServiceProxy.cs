using Asana.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asana.Maui.Util;
using Newtonsoft.Json;

namespace Asana.Library.Services
{
    public class ToDoServiceProxy
    {
        private List<ToDo> _toDoList = new List<ToDo>();
        public List<ToDo> ToDos { 
            get
            {
                return _toDoList.ToList();
            }

            private set {
                if (value != _toDoList)
                {
                    _toDoList = value;
                }
            }
        }

        private ToDoServiceProxy()
        {
            // ToDos = new List<ToDo>
            // {
            //     new ToDo{Id = 1, Name = "Task 1", Description = "My Task 1", IsCompleted=false},
            //     new ToDo{Id = 2, Name = "Task 2", Description = "My Task 2", IsCompleted=false },
            //     new ToDo{Id = 3, Name = "Task 3", Description = "My Task 3", IsCompleted=false },
            //     new ToDo{Id = 4, Name = "Task 4", Description = "My Task 4", IsCompleted=false },
            //     new ToDo{Id = 5, Name = "Task 5", Description = "My Task 5", IsCompleted=true }
            // };

            ToDos = new List<ToDo>();

            // var todoData = await new WebRequestHandler().Get("/ToDo");
            // ToDos = JsonConvert.DeserializeObject<List<ToDo>>(todoData) ?? new List<ToDo>();
        }

        public async Task InitializeAsync()
        {
            var todoData = await new WebRequestHandler().Get("/ToDo");
            ToDos = JsonConvert.DeserializeObject<List<ToDo>>(todoData) ?? new List<ToDo>();
        }

        private static ToDoServiceProxy? instance;

        

        public static ToDoServiceProxy Current
        {
            get
            {
                if(instance == null)
                {
                    instance = new ToDoServiceProxy();
                }

                return instance;
            }
        }
        public async Task<ToDo?> AddOrUpdate(ToDo? toDo)
        {
            if (toDo == null)
            {
                return null;
            }
            var isNewToDo = toDo.Id == 0;
            var todoData = await new WebRequestHandler().Post("/ToDo", toDo);
            var newToDo = JsonConvert.DeserializeObject<ToDo>(todoData);

            if (newToDo != null)
            {
                if (!isNewToDo)
                {
                    var existingToDo = _toDoList.FirstOrDefault(t => t.Id == newToDo.Id);
                    if (existingToDo != null)
                    {
                        var index = _toDoList.IndexOf(existingToDo);
                        _toDoList.RemoveAt(index);
                        _toDoList.Insert(index, newToDo);
                    }

                }
                else
                {
                    _toDoList.Add(newToDo);
                }
            }
            
            return toDo;
        }

        public void DisplayToDos(bool isShowCompleted = false)
        {
            if (isShowCompleted)
            {
                ToDos.ForEach(Console.WriteLine);
            }
            else
            {
                ToDos.Where(t => (t != null) && !(t?.IsCompleted ?? false))
                                .ToList()
                                .ForEach(Console.WriteLine);
            }
        }

        public ToDo? GetById(int id)
        {
            return ToDos.FirstOrDefault(t => t.Id == id);
        }

        public async Task DeleteToDo(int id)
        {
            if (id == 0)
            {
                return;
            }
            var todoData = await new WebRequestHandler().Delete($"/ToDo/{id}");
            var toDoToDelete = JsonConvert.DeserializeObject<ToDo>(todoData);
            if(toDoToDelete != null)
            {
                var localToDo = _toDoList.FirstOrDefault(t => t.Id == toDoToDelete.Id);
                if(localToDo != null)
                {
                    _toDoList.Remove(localToDo);
                }
            }
        }

    }
}