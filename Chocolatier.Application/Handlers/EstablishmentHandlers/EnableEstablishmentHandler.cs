using Chocolatier.Domain.Command.Establishment;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Chocolatier.Application.Handlers.EstablishmentHandlers
{
    public class EnableEstablishmentHandler : IRequestHandler<EnableEstablishmentCommand, Response>
    {
        private readonly UserManager<Establishment> UserManager;

        public EnableEstablishmentHandler(UserManager<Establishment> userManager)
        {
            UserManager = userManager;
        }

        public async Task<Response> Handle(EnableEstablishmentCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var establishment = await UserManager.FindByIdAsync(request.Id);

            if (establishment is null)
                return new Response(false, "Estabelecimento não encontrado tente novamente ou entre em contato com o suporte.", HttpStatusCode.BadRequest);

            var result = await UserManager.SetLockoutEnabledAsync(establishment, false);

            if (!result.Succeeded)
                return new Response(false, result.Errors.Select(e => e.Description).ToList(), HttpStatusCode.InternalServerError);

            return new Response(true, HttpStatusCode.OK);
        }
    }
}
