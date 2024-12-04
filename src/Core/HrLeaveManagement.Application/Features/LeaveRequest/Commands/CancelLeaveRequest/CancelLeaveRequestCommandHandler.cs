using HrLeaveManagement.Application.Contracts.Email;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Exceptions;
using HrLeaveManagement.Application.Models.Email;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler(
    ILeaveRequestRepository leaveRequestRepository,
    ILeaveAllocationRepository leaveAllocationRepository,
    IEmailSender emailSender)
    : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(leaveRequest), request.Id);

        leaveRequest.Cancelled = true;
        await leaveRequestRepository.UpdateAsync(leaveRequest);

        // if already approved, re-evaluate the employee's allocations for the leave type
        if (leaveRequest.Approved == true)
        {
            var daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
            var allocation = await leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId,
                leaveRequest.LeaveTypeId);

            if (allocation is not null)
            {
                allocation.NumberOfDays += daysRequested;

                await leaveAllocationRepository.UpdateAsync(allocation);
            }
        }

        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body =
                    $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled successfully.",
                Subject = "Leave Request Cancelled"
            };

            await emailSender.SendEmailAsync(email);
        }
        catch (Exception)
        {
            // log error
        }

        return Unit.Value;
    }
}