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
        public async Task<IActionResult> Patch([FromQuery] string EstablishmentId, UpdateEstablishmentCommand request, CancellationToken cancellationToken)
        {
            request.Id = EstablishmentId;
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
        public async Task<IActionResult> Delete([FromQuery] DeleteEstablishmentCommand request, CancellationToken cancellationToken)
        {
            return GetActionResult(await Mediator.Send(request, cancellationToken));
        }
    }
}
