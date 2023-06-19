using API.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Context;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }

    // Introduce the model to the database that eventually become an entity
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Profiling> Profilings { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Universitie> Universities { get; set; }

    // Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // One University has many Educations
        modelBuilder.Entity<Universitie>()
                    .HasMany(u => u.Educations)
                    .WithOne(e => e.Universitie)
                    .IsRequired(false)
                    .HasForeignKey(e => e.UniversityId)
                    .OnDelete(DeleteBehavior.SetNull);

        // One Education has one Profiling
        modelBuilder.Entity<Education>()
                    .HasOne(e => e.Profiling)
                    .WithOne(p => p.Education)
                    .IsRequired(false)
                    .HasForeignKey<Profiling>(p => p.EducationId)
                    .OnDelete(DeleteBehavior.SetNull);

        // One Profiling has one Employee
        modelBuilder.Entity<Employee>()
                    .HasOne(e => e.Profiling)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Profiling>(p => p.EmployeeNIK)
                    .OnDelete(DeleteBehavior.Restrict);

        // One Employee has one Account
        modelBuilder.Entity<Employee>()
                    .HasOne(e => e.Account)
                    .WithOne(a => a.Employee)
                    .IsRequired(false)
                    .HasForeignKey<Account>(a => a.EmployeeNIK)
                    .OnDelete(DeleteBehavior.Restrict);

        // One Account has many AccountRole??????
        modelBuilder.Entity<Account>()
                    .HasMany(e => e.AccountRoles)
                    .WithOne(a => a.Account)
                    .IsRequired(false)
                    .HasForeignKey(a => a.AccountNik)
                    .OnDelete(DeleteBehavior.Restrict);

        // Many AccountRole has one Role
        modelBuilder.Entity<AccountRole>()
                    .HasOne(a => a.Role)
                    .WithMany(r => r.AccountRoles)
                    .IsRequired(false)
                    .HasForeignKey(r => r.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
    }
}
