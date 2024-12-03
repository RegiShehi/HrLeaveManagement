using HrLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HrLeaveManagement.Identity.DbContext;

public class HrIdentityDatabaseContext(DbContextOptions<HrIdentityDatabaseContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(HrIdentityDatabaseContext).Assembly);
    }
}