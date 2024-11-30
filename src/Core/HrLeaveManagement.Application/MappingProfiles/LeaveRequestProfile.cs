using AutoMapper;
using Domain;
using HrLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HrLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HrLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HrLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

namespace HrLeaveManagement.Application.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequestListDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequestDetailsDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
        CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
        CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
    }
}