using Microsoft.EntityFrameworkCore;
using ToDoListify.API.Models.Domain;

namespace ToDoListify.API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Priority> Priorities { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User to ToDoItem relationship (1-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.ToDoItems)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Priority to ToDoItem relationship (1-to-many)
            modelBuilder.Entity<Priority>()
                .HasMany(p => p.ToDoItems)
                .WithOne(t => t.Priority)
                .HasForeignKey(t => t.PriorityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
