using Chocolatier.API.Authorization;
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
        [FactoryOrStoreAuthorization]
        [Route("List")]
        public async Task<IActionResult> GetList([FromQuery] GetRecipesPaginationRequest request, CancellationToken cancellationToken)
        {
            return GetActionResult(await RecipeQueries.GetRecipesPagination(request, cancellationToken));
        }

        [HttpGet]
        [FactoryAuthorization]
        [Route("Itens")]
        public async Task<IActionResult> GetItens([FromQuery] Guid Id, CancellationToken cancellationToken)
        {
            return GetActionResult(await RecipeQueries.GetRecipeItens(Id, cancellationToken));
        }

        [HttpPut]
        [FactoryAuthorization]
        [Route("{Id}")]
        public async Task<IActionResult> Patch([FromRoute] Guid Id, [FromBody] UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            request.Id = Id;
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpDelete]
        [FactoryAuthorization]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var request = new DeleteRecipeCommand { Id = Id };
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet]
        [FactoryAuthorization]
        [Route("{Id}/MissingIngredients")]
        public async Task<IActionResult> MissingIngredients([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            return GetActionResult(await RecipeQueries.GetMissingIngredientsFromRecipe(Id, cancellationToken));
        }

        [HttpPost]
        [FactoryAuthorization]
        [Route("{Id}/MakeRecipe")]
        public async Task<IActionResult> MakeRecipe([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var request = new MakeRecipeCommand { Id = Id };
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
