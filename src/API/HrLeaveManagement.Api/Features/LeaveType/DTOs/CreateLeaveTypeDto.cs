namespace HrLeaveManagement.Api.Features.LeaveType.DTOs;

public class CreateLeaveTypeDto
{
    public required string Name { get; set; }
    public int DefaultDays { get; set; }
}