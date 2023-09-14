namespace Transportathon.Infrastructure.Authentication;

public class AuthOptions
{
    public const string SectionName = "Auth";
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }

    public bool RequireHttpsMetadata { get; set; }
}