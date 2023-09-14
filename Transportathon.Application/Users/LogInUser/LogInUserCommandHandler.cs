using Transportathon.Application.Abstractions.Authentication;
using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Users;

namespace Transportathon.Application.Users.LogInUser;

internal sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public LogInUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(
        LogInUserCommand request,
        CancellationToken cancellationToken)
    {
        var validateCredentialResult =
            await _authenticationService.ValidateUserCredentials(request.Email, request.Password, cancellationToken);

        if (validateCredentialResult.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }

        var user = validateCredentialResult.Value;
        
        var result = _authenticationService.GetAccessTokenAsync(user.Id, user.Name.Value, request.Email,  user.Company != null);

        return result.IsFailure ? 
            Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials) : 
            new AccessTokenResponse(result.Value);
    }
}