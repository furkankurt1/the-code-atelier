using Business.Handlers.Task.Commands;
using Core.Utilities.ResultWrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IResult = Core.Utilities.ResultWrapper.IResult;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID claim not found.");

            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null)
                return Unauthorized("Role claim not found.");

            command.SetUserInfo(int.Parse(userIdClaim.Value), roleClaim.Value);

            IResult result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}