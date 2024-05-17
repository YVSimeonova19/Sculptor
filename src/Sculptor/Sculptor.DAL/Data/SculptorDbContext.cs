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

    // Set table relationships before being created
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Create relationship between Orders and Products
        modelBuilder
            .Entity<Order>()
            .HasMany(e => e.Products)
            .WithOne(e => e.Order)
            .HasForeignKey(e => e.OrderId)
            .IsRequired();

        // Create relationship between Timetables and Orders
        modelBuilder
            .Entity<Timetable>()
            .HasMany(e => e.Orders)
            .WithOne(e => e.Timetable)
            .HasForeignKey(e => e.TimetableId)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
