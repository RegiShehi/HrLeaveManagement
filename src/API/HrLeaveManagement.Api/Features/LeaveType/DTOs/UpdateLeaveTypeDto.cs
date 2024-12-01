namespace HrLeaveManagement.Api.Features.LeaveType.DTOs;

public class UpdateLeaveTypeDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int DefaultDays { get; set; }
}