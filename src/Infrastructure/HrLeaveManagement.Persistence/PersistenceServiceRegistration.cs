using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Persistence.DatabaseContext;
using HrLeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HrLeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<HrDatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

        return services;
    }
}