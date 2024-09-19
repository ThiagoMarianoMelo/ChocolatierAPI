using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : BaseController
    {
        public OrderController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [StoreAuthorization]
        public async Task<IActionResult> Post([FromBody] CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
