using AutoMapper;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Exceptions;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailQueryHandler(
    ILeaveRequestRepository leaveRequestRepository,
    IMapper mapper)
    : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
{
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request,
        CancellationToken cancellationToken)
    {
        var leaveRequest =
            mapper.Map<LeaveRequestDetailsDto>(await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));

        if (leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        // Add Employee details as needed

        return leaveRequest;
    }
}