using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.Establishment;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.RequestFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstablishmentController : BaseController
    {
        private readonly IEstablishmentQueries EstablishmentQueries;

        public EstablishmentController(IMediator mediator, IEstablishmentQueries establishmentQueries)
            : base(mediator)
        {
            EstablishmentQueries = establishmentQueries;
        }

        [HttpPost]
        [HeadquarterAuthorization]
        public async Task<IActionResult> Post(CreateEstablishmentCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpPatch]
        [HeadquarterAuthorization]
        [Route("{Id}")]
        public async Task<IActionResult> Patch([FromRoute] string Id, [FromBody] UpdateEstablishmentCommand request, CancellationToken cancellationToken)
        {
            request.Id = Id;
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet]
        [HeadquarterAuthorization]
        [Route("List")]
        public async Task<IActionResult> GetList([FromQuery] GetEstablishmentsPaginationsRequest request, CancellationToken cancellationToken)
        {
            return GetActionResult(await EstablishmentQueries.GetEstablishmentsPaginations(request, cancellationToken));
        }

        [HttpDelete]
        [HeadquarterAuthorization]
        [Route("Disable/{Id}")]
        public async Task<IActionResult> Disable([FromRoute] string Id, CancellationToken cancellationToken)
        {
            var request = new DisableEstablishmentCommand { Id = Id };
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }

        [HttpPut]
        [HeadquarterAuthorization]
        [Route("Enable/{Id}")]
        public async Task<IActionResult> Enable([FromRoute] string Id, CancellationToken cancellationToken)
        {
            var request = new EnableEstablishmentCommand { Id = Id };
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
