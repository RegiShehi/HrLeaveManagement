using HrLeaveManagement.Application.Contracts.Email;
using HrLeaveManagement.Application.Contracts.Logging;
using HrLeaveManagement.Application.Models.Email;
using HrLeaveManagement.Infrastructure.EmailService;
using HrLeaveManagement.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HrLeaveManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configure EmailSettings using options pattern
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}