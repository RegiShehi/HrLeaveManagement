﻿using Domain.Common;

namespace Domain;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public required string EmployeeId { get; set; }
    public int Period { get; set; }
}