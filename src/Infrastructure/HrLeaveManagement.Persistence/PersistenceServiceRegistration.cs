using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HrLeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}