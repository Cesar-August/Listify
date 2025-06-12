using Microsoft.EntityFrameworkCore;
using TodoListApp.Models;

namespace TodoListApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("USERS");
            modelBuilder.Entity<List>().ToTable("LISTS");
            modelBuilder.Entity<Card>().ToTable("CARDS");
        }
    }
}