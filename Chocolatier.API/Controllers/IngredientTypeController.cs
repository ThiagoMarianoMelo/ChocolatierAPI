using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.IngredientType;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientTypeController : BaseController
    {
        public IngredientTypeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [FactoryAuthorization]
        public async Task<IActionResult> Post(CreateIngredientTypeCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
