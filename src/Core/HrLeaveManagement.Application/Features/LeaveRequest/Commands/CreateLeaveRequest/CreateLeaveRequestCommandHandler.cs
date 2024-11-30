using AutoMapper;
using HrLeaveManagement.Application.Contracts.Email;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Exceptions;
using HrLeaveManagement.Application.Models.Email;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler(
    IEmailSender emailSender,
    IMapper mapper,
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveRequestRepository leaveRequestRepository)
    : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid Leave Request", validationResult);

        // Get requesting employee's id

        // Check on employee's allocation

        // if allocations aren't enough, return validation error with message

        // Create leave request
        var leaveRequest = mapper.Map<Domain.LeaveRequest>(request);
        await leaveRequestRepository.CreateAsync(leaveRequest);

        // send confirmation email
        var email = new EmailMessage
        {
            To = string.Empty, /* Get email from employee record */
            Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                   $"has been submitted successfully.",
            Subject = "Leave Request Submitted"
        };

        await emailSender.SendEmailAsync(email);

        return Unit.Value;
    }
}