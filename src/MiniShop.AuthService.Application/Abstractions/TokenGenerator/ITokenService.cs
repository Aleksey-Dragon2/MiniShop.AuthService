namespace MiniShop.AuthService.Application.Abstractions.TokenGenerator {
    public interface ITokenService
    {
        public Task<string> GenerateServiceTokenAsync(string secretKey, string issuer);
    }
} 