using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using To_Do_List_MVC_Refresher.Models;

namespace To_Do_List_MVC_Refresher.Controllers
{
    public class HomeController : Controller
    {
        // Static list = temporary in-memory storage, standing in for a database.
        // Resets every time the app restarts - a real database replaces this in Stage 2.
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { Id = Guid.NewGuid(), Title = "Buy milk", IsComplete = false },
            new TaskItem { Id = Guid.NewGuid(), Title = "Finish assignment", IsComplete = false }
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Shows all the tasks
        public IActionResult ToDoListHomePage()
        {
            return View(tasks);
        }

        /*Action methods allow us to return different results (return Views, Redirect, etc.), adding flexibility to controller methods.
        Represents what the controller sends back to the browser */
        [HttpPost]
        public IActionResult AddTask(string title)
        {
            var task = new TaskItem { Id = Guid.NewGuid(), Title = title, IsComplete = false };
            tasks.Add(task);

            if (TempData != null)
            {
                //TempData is a way of storing data for one request/redirect
                TempData["Success"] = "Task added successfully!";
            }

            //reloads the task list now including the new task - we use redirect to action when we don't want a new view to open
            return RedirectToAction("ToDoListHomePage");
        }

        // Toggle complete
        public IActionResult ToggleComplete(Guid id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsComplete = true;
            }
            return RedirectToAction("ToDoListHomePage");
        }

        // Delete task
        public IActionResult DeleteTask(Guid id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);

                if (TempData != null)
                {
                    TempData["Success"] = "Task deleted successfully!";
                }
            }
            return RedirectToAction("ToDoListHomePage");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}