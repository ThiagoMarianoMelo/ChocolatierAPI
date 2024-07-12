using Chocolatier.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Chocolatier.Application.Services
{
    public class AuthEstablishment : IAuthEstablishment
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public AuthEstablishment(IHttpContextAccessor context) => SetAuthUserData(context);

        private void SetAuthUserData(IHttpContextAccessor context)
        {
            Id = context.HttpContext?.User.FindFirst("EstablishmentId")?.Value ?? string.Empty;
            Email = context.HttpContext?.User.FindFirst("Email")?.Value ?? string.Empty;
        }
    }
}
