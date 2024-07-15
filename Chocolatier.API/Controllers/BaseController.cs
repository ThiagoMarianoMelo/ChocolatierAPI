using Chocolatier.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Chocolatier.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }   

        protected virtual IActionResult GetActionResult(Response result)
        {
            return result.HttpStatusCode switch
            {
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.BadRequest => BadRequest(result),
                HttpStatusCode.NotFound => NotFound(result),
                HttpStatusCode.OK => Ok(result),
                HttpStatusCode.Created => Created(),
                _ => StatusCode(500, result)
            };
        }
    }
}
