using Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Office> Offices => Set<Office>();
    public DbSet<Allocation> Allocations => Set<Allocation>();
    public DbSet<DepartmentLocation> DepartmentLocations => Set<DepartmentLocation>();
    public DbSet<ProjectOffice> ProjectOffices => Set<ProjectOffice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(builder =>
        {
            builder.HasKey(e => e.PersonalNumber);
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.MiddleName).HasMaxLength(100);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Address).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Gender).IsRequired().HasMaxLength(10);
            builder.Property(e => e.Salary).HasColumnType("decimal(18,2)");

            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);
        });

        modelBuilder.Entity<Department>(builder =>
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerPersonalNumber)
                .IsRequired(false);
        });

        modelBuilder.Entity<Project>(builder =>
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

            builder.HasOne(p => p.Department)
                .WithMany(d => d.Projects)
                .HasForeignKey(p => p.DepartmentId);
        });

        modelBuilder.Entity<Location>(builder =>
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(100);
            builder.Property(l => l.Address).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Office>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(o => o.Location)
                .WithMany(l => l.Offices)
                .HasForeignKey(o => o.LocationId);
        });

        modelBuilder.Entity<Allocation>(builder =>
        {
            builder.HasKey(a => new { a.EmployeePersonalNumber, a.ProjectId });

            builder.HasOne(a => a.Employee)
                .WithMany(e => e.Allocations)
                .HasForeignKey(a => a.EmployeePersonalNumber);

            builder.HasOne(a => a.Project)
                .WithMany(p => p.Allocations)
                .HasForeignKey(a => a.ProjectId);
        });

        modelBuilder.Entity<DepartmentLocation>(builder =>
        {
            builder.HasKey(dl => new { dl.DepartmentId, dl.LocationId });

            builder.HasOne(dl => dl.Department)
                .WithMany(d => d.DepartmentLocations)
                .HasForeignKey(dl => dl.DepartmentId);

            builder.HasOne(dl => dl.Location)
                .WithMany(l => l.DepartmentLocations)
                .HasForeignKey(dl => dl.LocationId);
        });

        modelBuilder.Entity<ProjectOffice>(builder =>
        {
            builder.HasKey(po => new { po.ProjectId, po.OfficeId });

            builder.HasOne(po => po.Project)
                .WithMany(p => p.ProjectOffices)
                .HasForeignKey(po => po.ProjectId);

            builder.HasOne(po => po.Office)
                .WithMany(o => o.ProjectOffices)
                .HasForeignKey(po => po.OfficeId);
        });
    }
}
