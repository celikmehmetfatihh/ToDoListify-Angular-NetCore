using System.ComponentModel.DataAnnotations;

namespace ToDoListify.API.Models.DTO
{
    public class CreateToDoItemRequestDto
    {
        [Required]
        public string Title { get; set; }

        public bool IsCompleted { get; set; } = false;
        public string? Detail { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public Guid? PriorityId { get; set; }
    }
}
