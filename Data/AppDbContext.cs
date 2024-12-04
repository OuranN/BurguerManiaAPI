using Microsoft.EntityFrameworkCore;
using BurguerManiaAPI.Users;
using BurguerManiaAPI.categories;
using BurguerManiaAPI.status;
using BurguerManiaAPI.Orders;
using BurguerManiaAPI.products;

namespace BurguerManiaAPI.Data;
public class AppDbContext : DbContext
{
    public required DbSet<User> User { get; set; }
    public required DbSet<Categories> Categories { get; set; }
    public required DbSet<Status> Status { get; set; }
    public required DbSet<Order> Orders { get; set; } 
    public required DbSet<Product> Product { get; set; } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source= Banco.sqlite");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração de relação 
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Status)
            .WithMany()
            .HasForeignKey(o => o.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}
