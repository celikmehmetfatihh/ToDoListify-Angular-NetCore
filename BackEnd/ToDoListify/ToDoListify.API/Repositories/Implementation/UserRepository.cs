using Microsoft.EntityFrameworkCore;
using ToDoListify.API.Data;
using ToDoListify.API.Models.Domain;
using ToDoListify.API.Repositories.Interface;

namespace ToDoListify.API.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await dbContext.Users.FindAsync(userId);
        }

        public async Task AddUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<string> GetUsernameByEmail(string email)
        {
            var user = await dbContext.Users
                                     .Where(u => u.Email == email)
                                     .FirstOrDefaultAsync();

            // Return the user's username or null if not found
            return user?.Username;
        }
    }
}
