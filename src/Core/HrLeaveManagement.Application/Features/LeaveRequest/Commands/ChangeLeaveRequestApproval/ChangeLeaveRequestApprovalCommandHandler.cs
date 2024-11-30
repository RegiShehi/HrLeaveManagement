using HrLeaveManagement.Application.Contracts.Email;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Exceptions;
using HrLeaveManagement.Application.Models.Email;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler(
    ILeaveRequestRepository leaveRequestRepository,
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


        // send confirmation email
        var email = new EmailMessage
        {
            To = string.Empty, /* Get email from employee record */
            Body =
                $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                $"has been updated.",
            Subject = "Leave Request Approval Status Updated"
        };

        await emailSender.SendEmailAsync(email);

        return Unit.Value;
    }
}