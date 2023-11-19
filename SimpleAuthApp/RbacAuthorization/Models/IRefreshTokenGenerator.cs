namespace JwtAuth.Models
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken();
    }
}
