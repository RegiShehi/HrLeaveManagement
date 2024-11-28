using AutoMapper;
using Domain;
using HrLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HrLeaveManagement.Application.MappingProfiles;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
    }
}