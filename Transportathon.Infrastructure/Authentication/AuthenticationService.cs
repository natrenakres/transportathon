using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Transportathon.Application.Abstractions.Authentication;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Users;

namespace Transportathon.Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly AuthOptions _authOptions;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IOptions<AuthOptions> options, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _authOptions = options.Value;
    }

    public async Task<Result<User>> ValidateUserCredentials(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<User>(UserErrors.InvalidCredentials);
        }

        if (user.Password != password)
        {
            return Result.Failure<User>(UserErrors.InvalidCredentials);
        }

        return user;
    }

    public Result<string> GetAccessTokenAsync(Guid userId, string name, string email, UserRole role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authOptions.Secret));
        var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new("sub", userId.ToString()),
            new("given_name", name),
            new("email", email)
        };

        switch (role)
        {
            case UserRole.Owner:
                claims.Add(new("role", "owner"));    
                break;
            case UserRole.Member:
                claims.Add(new("role", "member"));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(role), role, null);
        }


        var securityToken = new JwtSecurityToken(_authOptions.Issuer, _authOptions.Audience, claims, DateTime.Now,
            DateTime.Now.AddMonths(1), signinCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.InboundClaimTypeMap.Clear();
        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }
}