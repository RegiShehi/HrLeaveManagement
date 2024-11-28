using AutoMapper;
using HrLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<CreateLeaveTypeCommand, int>
{
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate incoming data

        // Convert to domain entity object
        var leaveTypeToCreate = mapper.Map<Domain.LeaveType>(request);

        // Add to database
        await leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        // Return record ID
        return leaveTypeToCreate.Id;
    }
}