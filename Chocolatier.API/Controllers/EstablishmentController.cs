﻿using Chocolatier.API.Authorization;
using Chocolatier.Domain.Command.Establishment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chocolatier.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstablishmentController : ControllerBase
    {
        private readonly IMediator Mediator;

        public EstablishmentController(IMediator mediator)
        {
            Mediator = mediator;
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
    }
}
