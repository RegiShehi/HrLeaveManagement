using AutoMapper;
using HrLeaveManagement.Application.Contracts.Logging;
using HrLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler(
    IMapper mapper,
    ILeaveTypeRepository leaveTypeRepository,
    IAppLogger<GetLeaveTypesQueryHandler> logger)
    : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        // Query database
        var leaveTypes = await leaveTypeRepository.GetAsync();

        // Convert data objects to DTO objects
        var data = mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        logger.LogInformation("Leave types were retrieved successfully.");
        // Return list of DTO objects
        return data;
    }
}