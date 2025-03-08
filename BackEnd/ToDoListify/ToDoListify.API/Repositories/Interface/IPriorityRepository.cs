using ToDoListify.API.Models.Domain;

namespace ToDoListify.API.Repositories.Interface
{
    public interface IPriorityRepository
    {
        Task<IEnumerable<Priority>> GetAllAsync();
    }
}
