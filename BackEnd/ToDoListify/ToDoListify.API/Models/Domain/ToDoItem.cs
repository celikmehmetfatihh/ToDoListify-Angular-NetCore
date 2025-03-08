namespace ToDoListify.API.Models.Domain
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string? Detail { get; set; }

        // Foreign key - Navigation Property for User
        public Guid UserId { get; set; }
        public User User { get; set; }

        // Foreign key - Navigation Property for Priority
        public Guid? PriorityId { get; set; }
        public Priority? Priority { get; set; }
    }
}
