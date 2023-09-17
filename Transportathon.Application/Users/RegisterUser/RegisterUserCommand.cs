using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Application.Users.LogInUser;

namespace Transportathon.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
        string Email,
        string Name,
        string Phone,
        string Password) : ICommand<AccessTokenResponse>;