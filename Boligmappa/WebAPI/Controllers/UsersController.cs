using Business.Handlers.User.Commands;
using Core.Utilities.ResultWrapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IResult = Core.Utilities.ResultWrapper.IResult;
using IDataResult = Core.Utilities.ResultWrapper.IDataResult<string>;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            IResult result = await _mediator.Send(command);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            IDataResult result = await _mediator.Send(command);

            if (result.Success)
                return Ok(result);

            return Unauthorized(result);
        }
    }
}