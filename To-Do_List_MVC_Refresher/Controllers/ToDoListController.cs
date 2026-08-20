using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using To_Do_List_MVC_Refresher.Models;

namespace To_Do_List_MVC_Refresher.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly ToDoDbContext _dbContext;

        //Constructor of the controller class
        public ToDoListController(ToDoDbContext dbContext)
        {
            _dbContext = dbContext; //
        }
        // Static list = temporary in-memory storage, standing in for a database.
        // Resets every time the app restarts - a real database replaces this in Stage 2.
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { Id = Guid.NewGuid(), Title = "Buy milk", IsComplete = true },
            new TaskItem { Id = Guid.NewGuid(), Title = "Finish assignment", IsComplete = false }
        };

        /* Has no attribute (default accepting GET requests) (viewing a list is just show me a page) */
        public IActionResult ToDoListHomePage()
        {
            var tasks = _dbContext.TaskItems.ToList();
            
            ViewBag.PendingCount = tasks.Count(t => !t.IsComplete);

            return View(tasks);
        }


        /*
         Does it cross a redirect? → TempData. 
         If Same request, but depends on data/logic the View can't compute? - ViewData/ViewBag. 
         Always exactly the same text? - skip all three, hardcode it.
         */
        [HttpPost]
        public IActionResult AddTask(string title)
        {
            var task = new TaskItem { Id = Guid.NewGuid(), Title = title, IsComplete = false };
            //tasks.Add(task);

            _dbContext.TaskItems.Add(task);
            _dbContext.SaveChanges();

            if (TempData != null)
            {
                TempData["Success"] = "Task added successfully!";
            }

            //reloads the task list now including the new task - we use redirect to action when we don't want a new view to open

            return RedirectToAction("ToDoListHomePage");


            ////return View("ToDoListHomePage", tasks); /* if a POST request directly renders a View (instead of redirecting), 
            //                                         * hitting refresh re-sends that same POST, silently adding the exact
            //                                         * same task again.*/
        }

        // Toggle complete
        public IActionResult ToggleComplete(Guid id)
        {
            //var task = tasks.FirstOrDefault(t => t.Id == id);
            var task = _dbContext.TaskItems.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsComplete = true;
                _dbContext.SaveChanges();
            }
            return RedirectToAction("ToDoListHomePage");
        }

       
        // Delete task
        [HttpPost]
        public IActionResult DeleteTask(Guid task_id)
        {
            // var task = tasks.FirstOrDefault(t => t.Id == task_id);
            var task = _dbContext.TaskItems.FirstOrDefault(t => t.Id == task_id);
            if (task != null)
            {
                _dbContext.TaskItems.Remove(task);
                _dbContext.SaveChanges();

                TempData["DeleteSuccess"] = "Task deleted successfully!";
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
