using HrLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HrLeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HrLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HrLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HrLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrLeaveManagement.Api.Features.LeaveAllocation;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> GetAllLeaveAllocations(bool isLoggedInUser = false)
    {
        var leaveAllocations = await mediator.Send(new GetLeaveAllocationListQuery());
        return Ok(leaveAllocations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> GetLeaveAllocationDetails(int id)
    {
        var leaveAllocation = await mediator.Send(new GetLeaveAllocationDetailQuery { Id = id });

        return Ok(leaveAllocation);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateLeaveAllocation(CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await mediator.Send(leaveAllocation);

        return CreatedAtAction(nameof(GetLeaveAllocationDetails), new { id = response }, new
        {
            id = response
        });
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateLeaveAllocation(UpdateLeaveAllocationCommand leaveAllocation)
    {
        await mediator.Send(leaveAllocation);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteLeaveAllocation(int id)
    {
        var command = new DeleteLeaveAllocationCommand { Id = id };
        await mediator.Send(command);

        return NoContent();
    }
}