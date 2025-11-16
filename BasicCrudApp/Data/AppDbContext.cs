using Microsoft.EntityFrameworkCore;
using BasicCrudApp.Models;

namespace BasicCrudApp.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FullName = "Admin", PhoneNumber = "0123456789", Address = "Address", Company = "Company", Email = "admin@gmail.com", Password = "admin", IsAdmin = true }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product", Description = "Sample Product", Price = 0 }
        );

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
}