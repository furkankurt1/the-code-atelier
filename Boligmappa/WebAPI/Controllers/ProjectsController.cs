using Core.Utilities.ResultWrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Business.Handlers.Project.Commands;
using Business.Handlers.Project.Queries;
using ICoreResult = Core.Utilities.ResultWrapper.IResult;

namespace WebAPI.Controllers
{
    [ApiController] [Route("api/[controller]")] [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProjectCommand command)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID claim not found.");

            command.SetCreatedBy(int.Parse(userIdClaim.Value));

            ICoreResult result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            var userId = int.Parse(userIdClaim.Value);

            var query = new GetProjectsQuery
            {
                UserId = userId
            };

            var result = await _mediator.Send(query);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}