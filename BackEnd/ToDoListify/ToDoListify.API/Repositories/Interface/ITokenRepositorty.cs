using ToDoListify.API.Models.Domain;

namespace ToDoListify.API.Repositories.Interface
{
    public interface ITokenRepositorty
    {
        string CreateJwtToken(User user);
    }
}
