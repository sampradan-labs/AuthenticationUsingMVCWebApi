namespace JwtAuth.Models
{
    public class RefreshCred
    {
        public string RefreshToken { get; set; }
        public string JwtToken { get; set; }
    }
}