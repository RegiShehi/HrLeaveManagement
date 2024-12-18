﻿using HrLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HrLeaveManagement.Application.Models.Identity;

namespace HrLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class LeaveRequestDetailsDto
{
    public int Id { get; set; }
    public required Employee Employee { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required string RequestingEmployeeId { get; set; }
    public required LeaveTypeDto LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public DateTime? DateActioned { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
}