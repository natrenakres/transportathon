using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Transportathon.Infrastructure.Authentication;

public class AuthOptionsSetup : IConfigureOptions<AuthOptions>
{
    private readonly IConfiguration _configuration;

    public AuthOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(AuthOptions options)
    {
        _configuration.GetSection(AuthOptions.SectionName).Bind(options);
    }
}