using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestesAPI.Models;

namespace TestesAPI.Data;

public class AppDBContext : IdentityDbContext<IdentityUser> // Enabling authentication and authorization with IdentityUser
{
    public AppDBContext(DbContextOptions<AppDBContext> options) :
        base(options)
    { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>().HasIndex(c => c.Email).IsUnique();
        modelBuilder.Entity<Vehicle>().HasIndex(v => v.Plate).IsUnique();
    }
}