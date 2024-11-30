using AutoMapper;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Exceptions;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler(
    IMapper mapper,
    ILeaveAllocationRepository leaveAllocationRepository,
    ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid Leave Allocation Request", validationResult);

        // Get Leave type for allocations
        var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        // Get Employees

        //Get Period

        //Assign Allocations
        var leaveAllocation = mapper.Map<Domain.LeaveAllocation>(request);
        await leaveAllocationRepository.CreateAsync(leaveAllocation);
        return Unit.Value;
    }
}