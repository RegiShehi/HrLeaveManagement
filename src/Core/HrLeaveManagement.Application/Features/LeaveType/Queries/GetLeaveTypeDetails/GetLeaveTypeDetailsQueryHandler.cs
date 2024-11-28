using AutoMapper;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Exceptions;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        // Query database
        var leaveType = await leaveTypeRepository.GetByIdAsync(request.Id);

        // Verify that record exists
        if (leaveType is null) throw new NotFoundException(nameof(LeaveType), request.Id);

        // Convert data objects to DTO objects
        var data = mapper.Map<LeaveTypeDetailsDto>(leaveType);

        // Return list of DTO objects
        return data;
    }
}