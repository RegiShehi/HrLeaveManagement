using HrLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HrLeaveManagement.Application.Models.Identity;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class LeaveRequestListDto
{
    public int Id { get; set; }
    public Employee? Employee { get; set; }
    public required string RequestingEmployeeId { get; set; }
    public required LeaveTypeDto LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool? Approved { get; set; }
}