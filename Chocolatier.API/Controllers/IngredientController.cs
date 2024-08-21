using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.Ingredient;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.RequestFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : BaseController
    {
        private readonly IIngredientQueries IngredientQueries;

        public IngredientController(IMediator mediator, IIngredientQueries ingredientQueries) : base(mediator)
        {
            IngredientQueries = ingredientQueries;
        }

        [HttpPost]
        [FactoryAuthorization]
        public async Task<IActionResult> Post([FromBody] CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet]
        [FactoryAuthorization]
        [Route("List")]
        public async Task<IActionResult> GetList([FromQuery] GetIngredientPaginationRequest request, CancellationToken cancellationToken)
        {
            return GetActionResult(await IngredientQueries.GetIngredientPagination(request, cancellationToken));
        }

        [HttpDelete]
        [FactoryAuthorization]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var request = new DeleteIngredientCommand { Id = Id };
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpPatch]
        [FactoryAuthorization]
        [Route("{Id}")]
        public async Task<IActionResult> Patch([FromRoute] Guid Id, [FromBody] UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            request.Id = Id;
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

    }
}
