namespace Chocolatier.Domain.ConfigObjects
{
    public class JWTConfig
    {
        public string? SecretKey { get; set; }
        public double ExpirationHours { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
