using Chocolatier.API.Authorization;
using Chocolatier.Application.Queries;
using Chocolatier.Domain.Command.Recipe;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.RequestFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : BaseController
    {
        private readonly IRecipeQueries RecipeQueries;

        public RecipeController(IMediator mediator, IRecipeQueries recipeQueries) : base(mediator)
        {
            RecipeQueries = recipeQueries;
        }

        [HttpPost]
        [FactoryAuthorization]
        public async Task<IActionResult> Post([FromBody] CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet]
        [FactoryAuthorization]
        [Route("List")]
        public async Task<IActionResult> GetList([FromQuery] GetRecipesPaginationRequest request, CancellationToken cancellationToken)
        {
            return GetActionResult(await RecipeQueries.GetRecipesPagination(request, cancellationToken));
        }
    }
}
