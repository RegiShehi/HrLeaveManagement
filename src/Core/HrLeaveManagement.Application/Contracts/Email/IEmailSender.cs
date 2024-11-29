using HrLeaveManagement.Application.Models.Email;

namespace HrLeaveManagement.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(EmailMessage email);
}