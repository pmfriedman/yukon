using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
            var s = await Task.Run(() => File.ReadAllLines(_fileLocation).ToList());

            return new TodoList {Todos = s.Select(t => new Todo {Title = t}).ToList()};
        }

        private async Task WriteFile(TodoList list)
        {
            await Task.Run(() => File.WriteAllLines(_fileLocation, list.Todos.Select(t => t.Title).ToArray()));
        }

        private static readonly string _fileLocation =
            System.Web.HttpContext.Current.Server.MapPath("~/App_Data/todo.txt");
    }
}