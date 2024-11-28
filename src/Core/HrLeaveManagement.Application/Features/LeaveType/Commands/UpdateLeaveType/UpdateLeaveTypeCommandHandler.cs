﻿using AutoMapper;
using HrLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate incoming data

        // Convert to domain entity object
        var leaveTypeToUpdate = mapper.Map<Domain.LeaveType>(request);

        // Add to database
        await leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        // Return Unit value
        return Unit.Value;
    }
}