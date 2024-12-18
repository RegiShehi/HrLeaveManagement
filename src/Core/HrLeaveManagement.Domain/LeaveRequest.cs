﻿using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain;

public class LeaveRequest : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestedComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
    public required string RequestingEmployeeId { get; set; }
}