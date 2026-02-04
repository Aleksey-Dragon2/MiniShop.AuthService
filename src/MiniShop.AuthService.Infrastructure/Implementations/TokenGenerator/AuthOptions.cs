namespace MiniShop.AuthService.Infrastructure.Implementations.TokenGenerator
{
    public class AuthOptions
    {
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public string SecretKey { get; init; } = string.Empty;
        public TimeSpan ExpirationTime { get; init; }
    }
}
