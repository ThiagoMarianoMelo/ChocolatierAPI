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
    public class EstablishmentController : ControllerBase
    {
        private readonly IMediator Mediator;
        private readonly IEstablishmentQueries EstablishmentQueries;

        public EstablishmentController(IMediator mediator, IEstablishmentQueries establishmentQueries)
        {
            Mediator = mediator;
            EstablishmentQueries = establishmentQueries;
        }

        [HttpPost]
        [HeadquarterAuthorization]
        public async Task<IActionResult> Create(CreateEstablishmentCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch]
        [HeadquarterAuthorization]
        public async Task<IActionResult> Patch(UpdateEstablishmentCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]   
        [HeadquarterAuthorization]
        [Route("List")]
        public async Task<IActionResult> GetPagination([FromQuery] GetEstablishmentsPaginationsRequest request, CancellationToken cancellationToken)
        {
            var result = await EstablishmentQueries.GetEstablishmentsPaginations(request, cancellationToken);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
