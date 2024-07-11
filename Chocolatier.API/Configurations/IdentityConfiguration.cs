using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Chocolatier.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentityCore<Establishment>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ChocolatierContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }
            );

            services.AddDataProtection();
        }
    }
}
