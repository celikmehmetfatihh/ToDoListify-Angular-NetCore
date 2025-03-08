namespace ToDoListify.API.Models.DTO
{
    public class PriorityDto
    {
        public Guid Id { get; set; }
        public string PriorityLevel { get; set; } // 'Low', 'Medium', 'High'
    }
}
