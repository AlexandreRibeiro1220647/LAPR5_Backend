using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Models.User;

namespace TodoApi.Infrastructure;

public class IPOContext : DbContext
{
    public IPOContext(DbContextOptions<IPOContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } 
    public DbSet<Patient> Patients { get; set; } 

     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users"); 
            
            modelBuilder.Entity<Patient>()
                .ToTable("Patients"); 
        }
    
}