using Chocolatier.Domain.Command.Auth;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Services;
using Chocolatier.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Chocolatier.Application.Handlers
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
                return new Response(false, new List<string> { "Login Inválido" });

            var resultCheckPassword = await UserManager.CheckPasswordAsync(establishment, request.Password ?? "");

            if (!resultCheckPassword)
                return new Response(false, new List<string> { "Login Inválido" });

            var token = await TokenService.GenerateTokenByEstablishment(establishment);

            return new Response(true, token);
        }
    }
}
