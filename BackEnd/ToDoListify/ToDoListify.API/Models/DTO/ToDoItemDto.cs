namespace ToDoListify.API.Models.DTO
{
    public class ToDoItemDto
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }

        public string Title { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string? Detail { get; set; }
        public Guid UserId { get; set; }
        public Guid? PriorityId { get; set; }
    }
}
