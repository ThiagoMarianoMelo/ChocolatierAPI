using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.Product;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.RequestFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductQueries ProductQueries;

        public ProductController(IMediator mediator, IProductQueries productQueries) : base(mediator)
        {
            ProductQueries = productQueries;
        }

        [HttpPost]
        [FactoryOrStoreAuthorization]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet]
        [FactoryOrStoreAuthorization]
        [Route("List")]
        public async Task<IActionResult> GetList([FromQuery] GetProductsPaginationRequest request, CancellationToken cancellationToken)
        {
            return GetActionResult(await ProductQueries.GetIngredientPagination(request, cancellationToken));
        }

        [HttpDelete]
        [FactoryOrStoreAuthorization]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var request = new DeleteProductCommand { Id = Id };
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpPatch]
        [FactoryOrStoreAuthorization]
        [Route("{Id}")]
        public async Task<IActionResult> Patch([FromRoute] Guid Id, [FromBody] UpdateProductCommand request, CancellationToken cancellationToken)
        {
            request.Id = Id;
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
