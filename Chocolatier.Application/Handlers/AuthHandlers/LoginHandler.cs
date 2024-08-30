using Chocolatier.Domain.Command.Auth;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Services;
using Chocolatier.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Chocolatier.Application.Handlers.AuthHandlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, Response>
    {
        private readonly ITokenService TokenService;
        private readonly UserManager<Establishment> UserManager;

        public LoginHandler(ITokenService tokenService, UserManager<Establishment> userManager)
        {
            TokenService = tokenService;
            UserManager = userManager;
        }

        public async Task<Response> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var establishment = await UserManager.FindByEmailAsync(request.Email ?? "");

            if (establishment == null)
                return new Response(false, ["Login Inválido"], HttpStatusCode.BadRequest);

            var resultCheckPassword = await UserManager.CheckPasswordAsync(establishment, request.Password ?? "");

            if (!resultCheckPassword)
                return new Response(false, ["Login Inválido"], HttpStatusCode.BadRequest);

            if (establishment.LockoutEnabled)
                return new Response(false, ["Conta do estabelecimento desativada, entre em contato com o administrador."], HttpStatusCode.BadRequest);

            var token = await TokenService.GenerateTokenByEstablishment(establishment);

            return new Response(true, token, HttpStatusCode.OK);
        }
    }
}
