using Microsoft.EntityFrameworkCore;

namespace To_Do_List_MVC_Refresher.Models
{
    /*
        This class inherits EF Core's database engine, accepts connection configuration 
        and hands it upward to that engine, and declares ONE property — TaskItems 
        — saying 'there's a table shaped like TaskItem.' 
        That's the entire class: three lines, doing exactly three jobs.
     */



    /*
     DbContext is the bridge between your C# code and the actual database. 
     It's the object that represents "a connection to this database, and everything I'm allowed to do with it"
     */
    public class ToDoDbContext : DbContext /* it inherits all of EF Core's built-in database logic (connecting, querying, saving)*/
    {
        /* Where does options actually come from? It's supplied automatically,
         * behind the scenes, by this line you wrote in Program.cs:*/
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }
        public DbSet<TaskItem> TaskItems { get; set; } /* DbSet<T> represents one table, 
                                                        * holding rows shaped like T. 
                                                        * So this specifically means: "there is a table of TaskItem-shaped rows."*/
    }
}
