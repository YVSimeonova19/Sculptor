using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sculptor.DAL.Models;

namespace Sculptor.DAL.Data;

public class SculptorDbContext : IdentityDbContext<User>
{
    public SculptorDbContext(DbContextOptions<SculptorDbContext> options) : base(options)
    {

    }

    // Create tables DbSets
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Timetable> Timetables { get; set; }
    public DbSet<ClientInfo> ClientInfo { get; set; }
    public DbSet<ProductOrder> ProductsOrders { get; set; }

    // Set table relationships before being created
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Create relationship between Orders and UserInfo
        modelBuilder
            .Entity<Order>()
            .HasOne(e => e.ClientInfo)
            .WithOne(e => e.Order)
            .HasForeignKey<ClientInfo>(e => e.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Make UserName unique
        modelBuilder
            .Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        // Set bridge table
        modelBuilder.Entity<ProductOrder>()
            .HasKey(x => new { x.ProductId, x.OrderId });

        // Create relationship between Products and Orders
        modelBuilder.Entity<ProductOrder>()
            .HasOne(po => po.Order)
            .WithMany()
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductOrder>()
            .HasOne(po => po.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
