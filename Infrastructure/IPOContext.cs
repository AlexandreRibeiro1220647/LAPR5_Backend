using Microsoft.EntityFrameworkCore;
using TodoApi.Models.User;
using TodoApi.Models.Patient;
using TodoApi.Infrastructure.Staff;
using TodoApi.Infrastructure.OperationRequest;
using TodoApi.Infrastructure.OperationType;
using TodoApi.Infrastructure.Patient;

namespace TodoApi.Infrastructure;

public class IPOContext : DbContext
{
    public IPOContext(DbContextOptions<IPOContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } 
    
    public DbSet<Models.OperationRequest.OperationRequest> OperationRequests { get; set; }

    public DbSet<Models.OperationType.OperationType> OperationTypes {get; set; }

    public DbSet<Models.Patient.Patient> Patients { get; set; }
    public DbSet<Models.Staff.Staff> Staffs { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OperationRequestConfiguration());
            modelBuilder.ApplyConfiguration(new OperationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());

        }
    
}