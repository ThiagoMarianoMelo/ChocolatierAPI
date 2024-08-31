using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseController
    {
        public ProductController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [FactoryOrStoreAuthorization]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
