using AutoMapper;
using HrLeaveManagement.Application.Contracts.Identity;
using HrLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler(
    ILeaveRequestRepository leaveRequestRepository,
    IUserService userService,
    IMapper mapper)
    : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request,
        CancellationToken cancellationToken)
    {
        List<Domain.LeaveRequest> leaveRequests;
        List<LeaveRequestListDto> requests;

        // Check if it is logged in employee
        if (request.IsLoggedInUser)
        {
            var userId = userService.UserId;
            leaveRequests = await leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

            var employee = await userService.GetEmployee(userId);
            requests = mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            foreach (var req in requests) req.Employee = employee;
        }
        else
        {
            leaveRequests = await leaveRequestRepository.GetLeaveRequestsWithDetails();
            requests = mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            foreach (var req in requests) req.Employee = await userService.GetEmployee(req.RequestingEmployeeId);
        }

        return requests;
    }
}