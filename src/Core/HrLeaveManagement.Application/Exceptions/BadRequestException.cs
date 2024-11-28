using FluentValidation.Results;

namespace HrLeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
        ValidationErrors = [];
    }

    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = [];

        foreach (var error in validationResult.Errors) ValidationErrors.Add(error.ErrorMessage);
    }

    public BadRequestException(List<string> validationErrors)
    {
        ValidationErrors = validationErrors;
    }

    private List<string> ValidationErrors { get; set; }
}