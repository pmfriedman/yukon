using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Yukon.Models;

namespace Yukon.Services
{
    public class TodoService
    {
        public void InitializeDataStore()
        {
            using (var file = new StreamWriter(_fileLocation, append: true))
            {
                file.Close();
            }
        }

        public async Task AddTodo(string todo)
        {
            var list = await ReadFile();
            list.Todos.Add(new Todo {Title = todo});
            await WriteFile(list);
        }

        public async Task<TodoList> GetAll()
        {
            return await ReadFile();
        }

        public async Task<List<string>> GetAllTitles()
        {
            var list = await GetAll();
            return list.Todos.Select(t => t.Title).ToList();
        }

        public async Task Delete(int index)
        {
            var list = await ReadFile();
            list.Todos.RemoveAt(index);
            await WriteFile(list);
        }

        private async Task<TodoList> ReadFile()
        {
            var json = await Task.Run(() => File.ReadAllText(_fileLocation));

            TodoList retVal = null;

            try
            {
                retVal = JsonConvert.DeserializeObject<TodoList>(json);
            }
            catch
            {
                
            }

            if (retVal == null)
            {
                retVal = new TodoList {Todos = new List<Todo>()};
            }

            return retVal;
        }

        private async Task WriteFile(TodoList list)
        {
            var json = JsonConvert.SerializeObject(list);

            await Task.Run(() => File.WriteAllText(_fileLocation, json));
        }

        private static readonly string _fileLocation =
            System.Web.HttpContext.Current.Server.MapPath("~/App_Data/todo.txt");
    }
}