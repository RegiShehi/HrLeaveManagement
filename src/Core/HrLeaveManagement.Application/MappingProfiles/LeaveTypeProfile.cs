using AutoMapper;
using Domain;
using HrLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HrLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HrLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HrLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

namespace HrLeaveManagement.Application.MappingProfiles;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        CreateMap<LeaveType, LeaveTypeDetailsDto>();
        CreateMap<CreateLeaveTypeCommand, LeaveType>();
        CreateMap<UpdateLeaveTypeCommand, LeaveType>();
    }
}