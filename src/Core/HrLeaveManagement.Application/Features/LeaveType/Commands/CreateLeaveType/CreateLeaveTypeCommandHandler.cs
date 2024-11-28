using AutoMapper;
using HrLeaveManagement.Application.Contracts.Persistence;
using HrLeaveManagement.Application.Exceptions;
using MediatR;

namespace HrLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<CreateLeaveTypeCommand, int>
{
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate incoming data
        var validator = new CreateLeaveTypeCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid) throw new BadRequestException("Invalid LeaveType", validationResult);

        // Convert to domain entity object
        var leaveTypeToCreate = mapper.Map<Domain.LeaveType>(request);

        // Add to database
        await leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        // Return record ID
        return leaveTypeToCreate.Id;
    }
}