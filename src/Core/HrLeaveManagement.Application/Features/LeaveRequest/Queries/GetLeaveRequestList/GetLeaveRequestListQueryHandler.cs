using AutoMapper;
using HrLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler(
    ILeaveRequestRepository leaveRequestRepository,
    IMapper mapper)
    : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request,
        CancellationToken cancellationToken)
    {
        // Check if it is logged in employee

        var leaveRequests = await leaveRequestRepository.GetLeaveRequestsWithDetails();
        var requests = mapper.Map<List<LeaveRequestListDto>>(leaveRequests);


        // Fill requests with employee information

        return requests;
    }
}