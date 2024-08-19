using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.Ingredient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : BaseController
    {
        public IngredientController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        //[FactoryAuthorization]
        public async Task<IActionResult> Post([FromBody] CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
