using AutoMapper;
using HrLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListHandler(
    ILeaveAllocationRepository leaveAllocationRepository,
    IMapper mapper)
    : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request,
        CancellationToken cancellationToken)
    {
        // To Add later
        // - Get records for specific user
        // - Get allocations per employee

        var leaveAllocations = await leaveAllocationRepository.GetLeaveAllocationsWithDetails();
        var allocations = mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        return allocations;
    }
}