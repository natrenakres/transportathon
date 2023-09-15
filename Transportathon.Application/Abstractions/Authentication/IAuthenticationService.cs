using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Users;

namespace Transportathon.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<Result<User>> ValidateUserCredentials(
        string email, string password,
        CancellationToken cancellationToken = default);

    Result<string> GetAccessTokenAsync(Guid userId, string name, string email,  UserRole role);
}