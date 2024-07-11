using Chocolatier.Domain.Command.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator Mediator;

        public AuthController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
