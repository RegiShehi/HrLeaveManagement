using AutoMapper;
using Domain;
using HrLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HrLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HrLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HrLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

namespace HrLeaveManagement.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
        CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
    }
}