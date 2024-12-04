using HrLeaveManagement.Application.Contracts.Email;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Exceptions;
using HrLeaveManagement.Application.Models.Email;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler(
    ILeaveRequestRepository leaveRequestRepository,
    ILeaveAllocationRepository leaveAllocationRepository,
    IEmailSender emailSender)
    : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        leaveRequest.Approved = request.Approved;
        await leaveRequestRepository.UpdateAsync(leaveRequest);

        // if request is approved, get and update the employee's allocations
        if (request.Approved)
        {
            var daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
            var allocation =
                await leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId,
                    leaveRequest.LeaveTypeId);
            if (allocation is not null)
            {
                allocation.NumberOfDays -= daysRequested;

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
                    $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated.",
                Subject = "Leave Request Approval Status Updated"
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