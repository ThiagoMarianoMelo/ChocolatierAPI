using Chocolatier.API.Authorization;
using Chocolatier.Application.Queries;
using Chocolatier.Domain.Command.Order;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.RequestFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : BaseController
    {
        private readonly IOrderQueries OrderQueries;
        public OrderController(IMediator mediator, IOrderQueries orderQueries) : base(mediator)
        {
            OrderQueries = orderQueries;
        }

        [HttpPost]
        [StoreAuthorization]
        public async Task<IActionResult> Post([FromBody] CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet]
        [FactoryOrStoreAuthorization]
        [Route("List")]
        public async Task<IActionResult> Get([FromQuery] GetOrdersPaginationRequest request, CancellationToken cancellationToken)
        {
            return GetActionResult(await OrderQueries.GetOrdersPagination(request, cancellationToken));
        }

        [HttpGet]
        [FactoryOrStoreAuthorization]
        [Route("Itens")]
        public async Task<IActionResult> GetItens([FromQuery] Guid Id, CancellationToken cancellationToken)
        {
            return GetActionResult(await OrderQueries.GetOrderItens(Id, cancellationToken));
        }

        [HttpPatch]
        [FactoryOrStoreAuthorization]
        [Route("ChangeStatus/{Id}")]
        public async Task<IActionResult> GetItens([FromRoute] Guid Id, [FromBody] ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            request.Id = Id;
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpDelete]
        [FactoryOrStoreAuthorization]
        [Route("{Id}")]
        public async Task<IActionResult> GetItens([FromRoute] Guid Id, [FromBody] CancelOrderCommand request, CancellationToken cancellationToken)
        {
            request.Id = Id;
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
