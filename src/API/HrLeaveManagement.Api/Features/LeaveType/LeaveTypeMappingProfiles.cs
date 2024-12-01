using HrLeaveManagement.Api.Features.LeaveType.DTOs;
using HrLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HrLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

namespace HrLeaveManagement.Api.Features.LeaveType;

public static class LeaveTypeMappingProfiles
{
    public static CreateLeaveTypeCommand ToCreateLeaveTypeCommand(this CreateLeaveTypeDto dto)
    {
        return new CreateLeaveTypeCommand
        {
            Name = dto.Name ?? string.Empty,
            DefaultDays = dto.DefaultDays
        };
    }

    public static UpdateLeaveTypeCommand ToUpdateLeaveTypeCommand(this UpdateLeaveTypeDto dto)
    {
        return new UpdateLeaveTypeCommand
        {
            Id = dto.Id,
            Name = dto.Name ?? string.Empty,
            DefaultDays = dto.DefaultDays
        };
    }
}