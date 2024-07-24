using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using ToDoList.Entities;

namespace ToDoList
{
    public class ListContext : DbContext
    {

        public ListContext(DbContextOptions<ListContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=TestDBLocal;Trusted_Connection=False;TrustServerCertificate=True;Encrypt=True;User Id=sa;Password=Password123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskList>()
                .HasOne<User>(task => task.User)
                .WithMany(user => user.TaskLists)
                .HasForeignKey(task => task.UserId);
        }
    }
}
