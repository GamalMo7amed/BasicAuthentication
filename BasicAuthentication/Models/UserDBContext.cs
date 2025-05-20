using BasicAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthenticationDemo.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Password = "password123" },
                new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Password = "password123" }
            );
        }
    }
}