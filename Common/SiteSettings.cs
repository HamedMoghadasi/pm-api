namespace Common
{
    public class SiteSettings
    {
        public JwtSettings JwtSettings { get; set; }
    }

    public class JwtSettings
    {
        public string secretkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationMinutes { get; set; }
        public string Encryptkey { get; set; }
        public bool ValidateIssuer { get; set; }
    }
}
