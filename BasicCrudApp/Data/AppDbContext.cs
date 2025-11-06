using Microsoft.EntityFrameworkCore;
using BasicCrudApp.Models;

namespace BasicCrudApp.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FullName = "John Doe", Email = "john@example.com", Password = "Password" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product", Description = "Sample Product", Price = 100 }
        );

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
}