using Chocolatier.Data.Context;
using Chocolatier.Domain.ConfigObjects;
using Chocolatier.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Chocolatier.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
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

            var JWTSettings = configuration.GetRequiredSection(nameof(JWTConfig)).Get<JWTConfig>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings!.SecretKey!)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.Configure<JWTConfig>(configuration.GetSection(nameof(JWTConfig)));
        }
    }
}
