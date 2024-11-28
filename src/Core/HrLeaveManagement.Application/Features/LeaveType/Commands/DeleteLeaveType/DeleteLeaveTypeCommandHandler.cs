using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Exceptions;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate incoming data

        // Retrieve domain entity object
        var leaveTypeToDelete = await leaveTypeRepository.GetByIdAsync(request.Id);

        // Verify that record exists
        if (leaveTypeToDelete is null) throw new NotFoundException(nameof(LeaveType), request.Id);

        // Remove from database
        await leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

        // Return Unit value
        return Unit.Value;
    }
}