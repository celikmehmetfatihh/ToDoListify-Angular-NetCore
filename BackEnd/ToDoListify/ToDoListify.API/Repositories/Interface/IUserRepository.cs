using ToDoListify.API.Models.Domain;

namespace ToDoListify.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid userId);
        Task<string> GetUsernameByEmail(string email);
        Task AddUserAsync(User user);

    }
}
