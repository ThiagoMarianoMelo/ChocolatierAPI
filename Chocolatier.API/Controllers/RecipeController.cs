using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.Recipe;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : BaseController
    {
        public RecipeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [FactoryAuthorization]
        public async Task<IActionResult> Post([FromBody] CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
