using HrLeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HrLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HrLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HrLeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HrLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HrLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HrLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrLeaveManagement.Api.Features.LeaveRequest;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class LeaveRequestsController(IMediator mediator) : ControllerBase
{
    // GET: api/<LeaveRequestsController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestListDto>>> GetAllLeaveRequests(bool isLoggedInUser = false)
    {
        var leaveRequests = await mediator.Send(new GetLeaveRequestListQuery());

        return Ok(leaveRequests);
    }

    // GET api/<LeaveRequestsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveRequestDetailsDto>> GetLeaveRequestDetails(int id)
    {
        var leaveRequest = await mediator.Send(new GetLeaveRequestDetailQuery { Id = id });
        return Ok(leaveRequest);
    }

    // POST api/<LeaveRequestsController>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateLeaveRequest(CreateLeaveRequestCommand leaveRequest)
    {
        var response = await mediator.Send(leaveRequest);
        return CreatedAtAction(nameof(GetLeaveRequestDetails), new { id = response }, new { id = response });
    }

    // PUT api/<LeaveRequestsController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateLeaveRequest(UpdateLeaveRequestCommand leaveRequest)
    {
        await mediator.Send(leaveRequest);

        return NoContent();
    }

    // PUT api/<LeaveRequestsController>/CancelRequest/
    [HttpPut]
    [Route("CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelLeaveRequest(CancelLeaveRequestCommand cancelLeaveRequest)
    {
        await mediator.Send(cancelLeaveRequest);

        return NoContent();
    }

    // PUT api/<LeaveRequestsController>/UpdateApproval/
    [HttpPut]
    [Route("UpdateApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateApprovalLeaveRequest(ChangeLeaveRequestApprovalCommand updateApprovalRequest)
    {
        await mediator.Send(updateApprovalRequest);

        return NoContent();
    }

    // DELETE api/<LeaveRequestsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteLeaveRequest(int id)
    {
        var command = new DeleteLeaveRequestCommand { Id = id };

        await mediator.Send(command);

        return NoContent();
    }
}