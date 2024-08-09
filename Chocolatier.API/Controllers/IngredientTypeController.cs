using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.IngredientType;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.RequestFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientTypeController : BaseController
    {
        private readonly IIngredientTypeQueries IngredientTypeQueries;

        public IngredientTypeController(IMediator mediator, IIngredientTypeQueries ingredientTypeQueries) : base(mediator)
        {
            IngredientTypeQueries = ingredientTypeQueries;
        }

        [HttpPost]
        [FactoryAuthorization]
        public async Task<IActionResult> Post([FromBody] CreateIngredientTypeCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet]
        [FactoryAuthorization]
        [Route("List")]
        public async Task<IActionResult> GetList([FromQuery] GetIngredientTypesPaginationsRequest request, CancellationToken cancellationToken)
        {
            return GetActionResult(await IngredientTypeQueries.GetIngredientTypesPagination(request, cancellationToken));
        }

        [HttpDelete]
        [FactoryAuthorization]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var request = new DeleteIngredientTypeCommand { Id = Id };
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
