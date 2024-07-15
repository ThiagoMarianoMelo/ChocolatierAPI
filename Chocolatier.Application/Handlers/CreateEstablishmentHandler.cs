using AutoMapper;
using Chocolatier.Domain.Command.Establishment;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Chocolatier.Application.Handlers
{
    public class CreateEstablishmentHandler : IRequestHandler<CreateEstablishmentCommand, Response>
    {
        private readonly IMapper Mapper;
        private readonly UserManager<Establishment> UserManager;

        public CreateEstablishmentHandler(IMapper mapper, UserManager<Establishment> userManager)
        {
            Mapper = mapper;
            UserManager = userManager;
        }

        public async Task<Response> Handle(CreateEstablishmentCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var establishmentByEmail = await UserManager.FindByEmailAsync(request.Email);

            if (establishmentByEmail != null)
                return new Response(false, ["Email já cadastrado."], HttpStatusCode.BadRequest);

            var establishment = Mapper.Map<Establishment>(request);

            var resultCreateEstablishment = await UserManager.CreateAsync(establishment, request.Password);

            if (!resultCreateEstablishment.Succeeded)
                return new Response(false, resultCreateEstablishment.Errors.Select(e => e.Description).ToList(), HttpStatusCode.BadRequest);

            var resultAddRole = await UserManager.AddToRoleAsync(establishment, establishment.EstablishmentType.ToString());

            if (!resultAddRole.Succeeded)
                return new Response(false, resultAddRole.Errors.Select(e => e.Description).ToList(), HttpStatusCode.InternalServerError);

            return new Response(true, HttpStatusCode.Created);
        }

    }
}
