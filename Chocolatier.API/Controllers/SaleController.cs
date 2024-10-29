using Chocolatier.API.Authorization;
using Chocolatier.Application.Queries;
using Chocolatier.Domain.Command.Sale;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.RequestFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : BaseController
    {
        private readonly ISalesQueries SalesQueries;
        public SaleController(IMediator mediator, ISalesQueries salesQueries) : base(mediator)
        {
            SalesQueries = salesQueries;
        }

        [HttpPost]
        [StoreAuthorization]
        public async Task<IActionResult> Post([FromBody] CreateSaleCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet]
        [StoreAuthorization]
        [Route("List")]
        public async Task<IActionResult> Get([FromQuery] GetSalesPaginationsRequest request, CancellationToken cancellationToken)
        {
            return GetActionResult(await SalesQueries.GetSalesPagination(request, cancellationToken));
        }

        [HttpGet]
        [StoreAuthorization]
        [Route("Itens")]
        public async Task<IActionResult> GetItens([FromQuery] Guid Id, CancellationToken cancellationToken)
        {
            return GetActionResult(await SalesQueries.GetSaleItens(Id, cancellationToken));
        }
    }
}
