using HrLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HrLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HrLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
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
    public async Task<ActionResult<List<LeaveTypeDto>>> GetAllLeaveTypes()
    {
        var leaveTypes = await mediator.Send(new GetLeaveTypesQuery());

        return Ok(leaveTypes);
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

        return CreatedAtAction(nameof(GetLeaveTypeDetails), new { id = leaveType }, new
        {
            id = leaveType
        });
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateLeaveType(UpdateLeaveTypeCommand leaveTypeCommand)
    {
        await mediator.Send(leaveTypeCommand);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteLeaveType(int id)
    {
        var command = new DeleteLeaveTypeCommand { Id = id };
        await mediator.Send(command);

        return NoContent();
    }
}