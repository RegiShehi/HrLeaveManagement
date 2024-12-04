using Domain;
using Domain.Common;
using HrLeaveManagement.Application.Contracts.Identity;
using Microsoft.EntityFrameworkCore;

namespace HrLeaveManagement.Persistence.DatabaseContext;

public class HrDatabaseContext(DbContextOptions<HrDatabaseContext> options, IUserService userService)
    : DbContext(options)
{
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDatabaseContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(q => q.State is EntityState.Added or EntityState.Modified))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;
                entry.Entity.CreatedBy = userService.UserId;
            }

            entry.Entity.DateModified = DateTime.UtcNow;
            entry.Entity.ModifiedBy = userService.UserId;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}