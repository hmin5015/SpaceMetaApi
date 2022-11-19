namespace SpaceMetaApi.Configuration;

public class JwtConfig
{
    public string Secret { get; set; } = string.Empty;

    public int RefreshTokenTTL { get; set; }

    public string Issuer { get; set; } = string.Empty;
}
