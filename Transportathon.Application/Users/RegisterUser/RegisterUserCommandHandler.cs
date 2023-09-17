using Transportathon.Application.Abstractions.Authentication;
using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Application.Users.LogInUser;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Users;

namespace Transportathon.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, AccessTokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthenticationService _authenticationService;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _authenticationService = authenticationService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = User.Create(
            new Name(request.Name),
            new Email(request.Email),
            new Phone(request.Phone),
            request.Password,
            UserRole.Member);

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = _authenticationService.GetAccessTokenAsync(user.Id, user.Name.Value, request.Email,  user.Role);

        var isOwner = user.Role == UserRole.Owner;

        return result.IsFailure ? 
            Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials) : 
            new AccessTokenResponse(result.Value, user.Name.Value, isOwner);
    }
}