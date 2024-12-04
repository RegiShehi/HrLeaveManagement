using HrLeaveManagement.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HrLeaveManagement.Api;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Handle validation errors when custom validation is bypassed
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = (context) =>
            {
                var errors = context.ModelState
                    .Where(ms => ms.Value is { Errors.Count: > 0 })
                    .ToDictionary(
                        ms => ms.Key,
                        ms => ms.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? []
                    );

                // Check for JSON deserialization error
                var jsonDeserializationErrors = errors.Values
                    .SelectMany(e => e)
                    .Where(e => e.Contains("JSON deserialization", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (jsonDeserializationErrors.Count > 0)
                    // Throw a generic BadRequestException with the JSON deserialization message
                    // throw new BadRequestException("Incorrect payload", new Dictionary<string, string[]>
                    // {
                    //     { "JSONSerializer", jsonDeserializationErrors.ToArray() }
                    // });

                    throw new BadRequestException("Incorrect JSON payload", new Dictionary<string, string[]>
                    {
                        { "JSONSerializer", ["Please validate JSON payload"] }
                    });


                throw new BadRequestException("Validation error", errors);
            };
        });

        return services;
    }
}