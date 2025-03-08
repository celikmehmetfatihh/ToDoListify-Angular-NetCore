using Microsoft.EntityFrameworkCore;
using ToDoListify.API.Data;
using ToDoListify.API.Models.Domain;
using ToDoListify.API.Repositories.Interface;

namespace ToDoListify.API.Repositories.Implementation
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ToDoItemRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ToDoItem> CreateAsync(ToDoItem item)
        {
            await dbContext.ToDoItems.AddAsync(item);
            await dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<IEnumerable<ToDoItem>> GetPendingToDoItemsByUserIdAsync(Guid userId)
        {
            return await dbContext.ToDoItems
                .Where(item => !item.IsCompleted && item.UserId == userId)
                .OrderBy(item =>
                    item.PriorityId == new Guid("19CB0293-9F7D-4927-9FEE-8E14976219CF") ? 1 :
                    item.PriorityId == new Guid("BB1C7A1D-2668-4E1A-B2BE-3EB821FE20EC") ? 2 :
                    item.PriorityId == new Guid("C98EDD76-0C62-47F0-8979-C478C679CC27") ? 3 :
                    4) // 4 for 'null' or any unknown value
                .ToListAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetCompletedToDoItemsByUserIdAsync(Guid userId)
        {
            return await dbContext.ToDoItems
                .Where(item => item.IsCompleted && item.UserId == userId)
                .OrderBy(item =>
                    item.PriorityId == new Guid("19CB0293-9F7D-4927-9FEE-8E14976219CF") ? 1 :
                    item.PriorityId == new Guid("BB1C7A1D-2668-4E1A-B2BE-3EB821FE20EC") ? 2 :
                    item.PriorityId == new Guid("C98EDD76-0C62-47F0-8979-C478C679CC27") ? 3 :
                    4) // 4 for 'null' or any unknown value
                .ToListAsync();
        }

        public async Task<ToDoItem?> GetById(Guid id)
        {
            return await dbContext.ToDoItems.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<ToDoItem?> UpdateAsync(ToDoItem item)
        {
            var existingItem = await dbContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == item.Id);

            if (existingItem != null)
            {
                dbContext.Entry(existingItem).CurrentValues.SetValues(item);
                await dbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }

        public async Task<ToDoItem?> DeleteAsync(Guid id)
        {
            var existingItem = await dbContext.ToDoItems.FirstOrDefaultAsync (x => x.Id == id);

            if (existingItem == null)
                return null;

            dbContext.ToDoItems.Remove(existingItem);
            await dbContext.SaveChangesAsync();
            return existingItem;
        }
    }
}
