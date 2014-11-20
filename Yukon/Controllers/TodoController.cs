using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Yukon.Services;

namespace Yukon.Controllers
{
    public class TodoController : Controller
    {
        public TodoController()
        {
            TodoService = new TodoService();
        }

        public TodoService TodoService { get; set; }

        // GET: Todo
        //[RequireHttps]
        public ActionResult Index()
        {
            return View();
        }
    
        public async Task<JsonResult> GetAll()
        {
            var all = await TodoService.GetAll();
            return Json(all, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddTodo(string todo)
        {
            await TodoService.AddTodo(todo);

            return Json("Success");
        }
    }
}