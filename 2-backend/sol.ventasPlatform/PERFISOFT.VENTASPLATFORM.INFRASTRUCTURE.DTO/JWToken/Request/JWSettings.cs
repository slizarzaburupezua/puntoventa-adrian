namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.JWToken.Request
{
    public class JWSettings
    {
        public string SecretKey { get; init; } = null!;

        public int ExpiryMinutes { get; init; }

        public string Issuer { get; init; } = null!;

        public string Audience { get; init; } = null!;

    }
}
