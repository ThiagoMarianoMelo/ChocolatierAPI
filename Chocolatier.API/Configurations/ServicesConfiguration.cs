using Chocolatier.Application.Services;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Services;

namespace Chocolatier.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IAuthEstablishment, AuthEstablishment>();
        }
    }
}
