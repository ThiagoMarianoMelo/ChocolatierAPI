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
    }
}
