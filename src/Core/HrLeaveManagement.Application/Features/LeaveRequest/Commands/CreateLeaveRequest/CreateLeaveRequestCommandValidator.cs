using FluentValidation;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Features.LeaveRequest.Shared;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
{
    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        Include(new BaseLeaveRequestValidator(leaveTypeRepository));
    }
}