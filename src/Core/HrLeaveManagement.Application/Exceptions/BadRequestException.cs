using FluentValidation.Results;

namespace HrLeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = validationResult.ToDictionary();
    }

    public BadRequestException(string message, Dictionary<string, string[]> errors) : base(message)
    {
        ValidationErrors = errors;
    }

    public IDictionary<string, string[]> ValidationErrors { get; set; }
}