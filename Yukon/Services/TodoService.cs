using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
            using (var file = new StreamWriter(_fileLocation, append: true))
            {
                await file.WriteLineAsync(todo);
                file.Close();
            }
        }

        public async Task<List<string>> GetAll()
        {
            List<string> todos = new List<string>();
            using (var file = new StreamReader(_fileLocation))
            {
                string line;
                while ((line = await file.ReadLineAsync()) != null)
                {
                    todos.Add(line);
                }

                file.Close();
            }

            return todos;

        }

        private static readonly string _fileLocation =
            System.Web.HttpContext.Current.Server.MapPath("~/App_Data/todo.txt");
    }
}