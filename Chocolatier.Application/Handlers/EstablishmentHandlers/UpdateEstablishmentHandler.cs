using Chocolatier.Domain.Command.Establishment;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Chocolatier.Application.Handlers.EstablishmentHandlers
{
    public class UpdateEstablishmentHandler : IRequestHandler<UpdateEstablishmentCommand, Response>
    {
        private readonly UserManager<Establishment> UserManager;

        public UpdateEstablishmentHandler(UserManager<Establishment> userManager)
        {
            UserManager = userManager;
        }

        public async Task<Response> Handle(UpdateEstablishmentCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var establishment = await UserManager.FindByIdAsync(request.Id);

            if (establishment is null)
                return new Response(false, "Estabelecimento não encontrado tente novamente ou entre em contato com o suporte.", HttpStatusCode.BadRequest);

            var resultChangeData = await ChangeDataFromEstablishment(establishment, request);

            if (!resultChangeData.Succeeded)
                return new Response(false, resultChangeData.Errors.Select(e => e.Description).ToList(), HttpStatusCode.BadRequest);

            return new Response(true, HttpStatusCode.OK);
        }

        private async Task<IdentityResult> ChangeDataFromEstablishment(Establishment establishment, UpdateEstablishmentCommand request)
        {

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                var tokenChangePassword = await UserManager.GeneratePasswordResetTokenAsync(establishment);

                var resultChangePassword = await UserManager.ResetPasswordAsync(establishment, tokenChangePassword, request.Password);

                if (!resultChangePassword.Succeeded)
                    return resultChangePassword;
            }

            if (!string.IsNullOrWhiteSpace(request.UserName))
                establishment.UserName = request.UserName;

            if (!string.IsNullOrWhiteSpace(request.Email))
                establishment.Email = request.Email;

            if (!string.IsNullOrWhiteSpace(request.Address))
                establishment.Address = request.Address;

            return await UserManager.UpdateAsync(establishment);
        }
    }
}
