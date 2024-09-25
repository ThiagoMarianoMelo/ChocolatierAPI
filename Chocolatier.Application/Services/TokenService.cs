using Chocolatier.Domain.ConfigObjects;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chocolatier.Application.Services
{
    public class TokenService : ITokenService
    {

        private readonly UserManager<Establishment> UserManager;
        private readonly JWTConfig JWTConfig;

        public TokenService(UserManager<Establishment> userManager, IOptions<JWTConfig> JwtOptions)
        {
            UserManager = userManager;
            JWTConfig = JwtOptions.Value;
        }

        public async Task<string> GenerateTokenByEstablishment(Establishment establishment)
        {
            var tokenDescriptor = await GetTokenDescriptorByEstablishment(establishment);

            return GenerateJwtToken(tokenDescriptor);
        }

        private async Task<SecurityTokenDescriptor> GetTokenDescriptorByEstablishment(Establishment establishment)
        {

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaimsIdentity(establishment),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConfig.SecretKey ?? "")),
                                                SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddHours(JWTConfig.ExpirationHours),
                Issuer = JWTConfig.Issuer,
                Audience = JWTConfig.Audience
            };

            var userRoles = await UserManager.GetRolesAsync(establishment);
            tokenDescriptor.Subject.AddClaims(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            return tokenDescriptor;
        }

        private static ClaimsIdentity GenerateClaimsIdentity(Establishment establishment) => new(claims: [
                new ("EstablishmentId",establishment.Id.ToString() ?? ""),
                new ("Email",establishment.Email ?? ""),
                new ("establishmentType", establishment.EstablishmentType.ToString())
            ]);

        private static string GenerateJwtToken(SecurityTokenDescriptor tokenDescriptor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}
