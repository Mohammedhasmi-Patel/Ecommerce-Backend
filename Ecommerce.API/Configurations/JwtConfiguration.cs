namespace Ecommerce.API.Configurations;

public class JwtConfiguration
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public int JwtExpireMinutes { get; set; }
}
