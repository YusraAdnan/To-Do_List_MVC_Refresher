namespace To_Do_List_MVC_Refresher.Models
{
    public partial class TaskItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public bool IsComplete { get; set; }
    }
}