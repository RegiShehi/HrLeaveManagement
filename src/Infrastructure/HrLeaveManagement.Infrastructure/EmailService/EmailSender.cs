﻿using HrLeaveManagement.Application.Contracts.Email;
using HrLeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HrLeaveManagement.Infrastructure.EmailService;

public class EmailSender(IOptions<EmailSettings> emailSettings) : IEmailSender
{
    private EmailSettings EmailSettings { get; } = emailSettings.Value;

    public async Task<bool> SendEmailAsync(EmailMessage email)
    {
        var client = new SendGridClient(EmailSettings.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress
        {
            Email = EmailSettings.FromAddress,
            Name = EmailSettings.FromName
        };

        var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}