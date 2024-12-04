using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQuery : IRequest<List<LeaveRequestListDto>>
{
    public bool IsLoggedInUser { get; set; }
}