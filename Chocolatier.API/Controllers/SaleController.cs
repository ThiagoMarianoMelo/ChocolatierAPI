using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.Sale;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : BaseController
    {
        public SaleController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [StoreAuthorization]
        public async Task<IActionResult> Post([FromBody] CreateSaleCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
