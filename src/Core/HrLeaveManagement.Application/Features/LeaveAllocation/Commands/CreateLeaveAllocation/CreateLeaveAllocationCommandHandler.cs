using HrLeaveManagement.Application.Contracts.Identity;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Exceptions;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler(
    ILeaveAllocationRepository leaveAllocationRepository,
    IUserService userService,
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

        if (leaveType is null)
            throw new NotFoundException(nameof(LeaveType), request.LeaveTypeId);

        // Get Employees
        var employees = await userService.GetEmployees();

        //Get Period
        var period = DateTime.Now.Year;

        //Assign Allocations IF an allocation doesn't already exist for period and leave type
        var allocations = new List<Domain.LeaveAllocation>();

        foreach (var emp in employees)
        {
            var allocationExists =
                await leaveAllocationRepository.AllocationExists(emp.Id, request.LeaveTypeId, period);

            if (allocationExists is false)
                allocations.Add(new Domain.LeaveAllocation
                {
                    EmployeeId = emp.Id,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period
                });
        }

        if (allocations.Count > 0) await leaveAllocationRepository.AddAllocations(allocations);

        return Unit.Value;
    }
}