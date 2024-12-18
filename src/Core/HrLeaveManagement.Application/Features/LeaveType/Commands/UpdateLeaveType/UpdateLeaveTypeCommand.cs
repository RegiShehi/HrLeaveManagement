﻿using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int DefaultDays { get; set; }
}