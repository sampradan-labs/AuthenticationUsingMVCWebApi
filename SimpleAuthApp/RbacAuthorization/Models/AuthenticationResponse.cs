namespace JwtAuth.Models
{
    public class AuthenticationResponse
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
