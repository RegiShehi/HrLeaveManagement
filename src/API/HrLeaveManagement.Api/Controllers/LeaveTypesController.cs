using HrLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HrLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HrLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrLeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<List<LeaveTypeDto>> GetAllLeaveTypes()
    {
        var leaveTypes = await mediator.Send(new GetLeaveTypesQuery());

        return leaveTypes;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveTypeDetailsDto>> GetLeaveTypeDetails(int id)
    {
        var leaveType = await mediator.Send(new GetLeaveTypeDetailsQuery(id));

        return Ok(leaveType);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateLeaveType(CreateLeaveTypeCommand leaveTypeCommand)
    {
        var leaveType = await mediator.Send(leaveTypeCommand);

        return CreatedAtAction(nameof(GetLeaveTypeDetails), new { id = leaveType });
    }
}