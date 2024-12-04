using AutoMapper;
using HrLeaveManagement.Application.Contracts.Email;
using HrLeaveManagement.Application.Contracts.Identity;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Exceptions;
using HrLeaveManagement.Application.Models.Email;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler(
    IEmailSender emailSender,
    IMapper mapper,
    IUserService userService,
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveAllocationRepository leaveAllocationRepository,
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
        var employeeId = userService.UserId;

        // Check on employee's allocation
        var allocation = await leaveAllocationRepository.GetUserAllocations(employeeId, request.LeaveTypeId);

        // if allocations aren't enough, return validation error with message
        if (allocation is null)
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveTypeId),
                "You do not have any allocations for this leave type."));

            throw new BadRequestException("Invalid Leave Request", validationResult);
        }

        var daysRequested = (int)(request.EndDate - request.StartDate).TotalDays;

        if (daysRequested > allocation.NumberOfDays)
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                nameof(request.EndDate), "You do not have enough days for this request"));

            throw new BadRequestException("Invalid Leave Request", validationResult);
        }

        // Create leave request
        var leaveRequest = mapper.Map<Domain.LeaveRequest>(request);
        leaveRequest.RequestingEmployeeId = employeeId;
        leaveRequest.DateRequested = DateTime.UtcNow;

        await leaveRequestRepository.CreateAsync(leaveRequest);

        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                       $"has been submitted successfully.",
                Subject = "Leave Request Submitted"
            };

            await emailSender.SendEmailAsync(email);
        }
        catch (Exception)
        {
            //// Log or handle error,
        }

        return Unit.Value;
    }
}