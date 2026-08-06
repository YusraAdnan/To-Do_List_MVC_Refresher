using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using To_Do_List_MVC_Refresher.Models;

namespace To_Do_List_MVC_Refresher.Controllers
{
    public class ToDoListController : Controller
    {

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
            return View(tasks);
        }
        /* A request is a message your browser sends to the server, asking it to do something
         * — either "show me this page" (GET) or "here's some data, do something with it" 
         * (POST). Every click, form submission, or typed URL sends one behind the scenes; 
         * the server receives it, does whatever it says, and sends a response back.*/


        /* Action methods allow us to return different results (return Views, Redirect, etc.), adding flexibility to controller methods.
        Represents what the controller sends back to the browser */

        /* Without [HttpPost]: the action accepts requests two ways 
         * — someone clicking a link, or someone visiting a URL directly. 
         * Anyone can trigger it just by typing the URL into a browser, no form, no button, no real intent needed.

         With [HttpPost]: the action only runs if the request came in as a POST 
        — meaning it had to come from an actual form submission. 
        Just visiting the URL directly gets rejected (a 405 error) — it won't run at all.*/
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
            ////return View("ToDoListHomePage", tasks); /* if a POST request directly renders a View (instead of redirecting), 
            //                                         * hitting refresh re-sends that same POST, silently adding the exact
            //                                         * same task again.*/
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

        /*
         "A POST request is safer because it can only be triggered by a genuine form submission. 
        A GET request, on the other hand, can be triggered just by visiting a 
        URL — manually typed, clicked, or even accessed automatically by something like a crawler. 
        Since deleting data requires real intent, using GET for a delete action risks it 
        being triggered accidentally, with no real intent behind it. Using POST ensures it 
        can only happen through a deliberate form submission."*/
        // Delete task
        [HttpPost]
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
