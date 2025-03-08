using Microsoft.EntityFrameworkCore;
using ToDoListify.API.Data;
using ToDoListify.API.Models.Domain;
using ToDoListify.API.Repositories.Interface;

namespace ToDoListify.API.Repositories.Implementation
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly ApplicationDbContext dbContext;

        public PriorityRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Priority>> GetAllAsync()
        {
            return await dbContext.Priorities.ToListAsync();
        }
    }
}
