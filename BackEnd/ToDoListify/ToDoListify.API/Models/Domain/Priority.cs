namespace ToDoListify.API.Models.Domain
{
    public class Priority
    {
        public Guid Id { get; set; }
        public string PriorityLevel { get; set; } // 'Low', 'Medium', 'High'

        // Navigation property
        public ICollection<ToDoItem> ToDoItems { get; set; }
    }
}
