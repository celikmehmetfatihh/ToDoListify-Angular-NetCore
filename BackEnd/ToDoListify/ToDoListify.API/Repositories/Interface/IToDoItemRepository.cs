using ToDoListify.API.Models.Domain;

namespace ToDoListify.API.Repositories.Interface
{
    public interface IToDoItemRepository
    {
        Task<ToDoItem> CreateAsync(ToDoItem item);
        Task<IEnumerable<ToDoItem>> GetPendingToDoItemsByUserIdAsync(Guid userId);
        Task<IEnumerable<ToDoItem>> GetCompletedToDoItemsByUserIdAsync(Guid userId);
        Task<ToDoItem?> GetById(Guid id);
        Task<ToDoItem?> UpdateAsync(ToDoItem item);
        Task<ToDoItem?> DeleteAsync(Guid id);
    }
}
