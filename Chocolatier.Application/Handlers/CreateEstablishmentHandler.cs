using AutoMapper;
using Chocolatier.Domain.Command;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

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

            var establishment = Mapper.Map<Establishment>(request);

            var resultCreateEstablishment = await UserManager.CreateAsync(establishment);

            if (!resultCreateEstablishment.Succeeded)
                return new Response(false, resultCreateEstablishment.Errors.Select(e => e.Description).ToList());

            var resultAddRole = await UserManager.AddToRoleAsync(establishment, establishment.EstablishmentType.ToString());

            if (!resultAddRole.Succeeded)
                return new Response(false, resultAddRole.Errors.Select(e => e.Description).ToList());

            return new Response(true);
        }

    }
}
