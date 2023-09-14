using Transportathon.Application.Abstractions.Messaging;

namespace Transportathon.Application.Users.LogInUser;



public sealed record LogInUserCommand(string Email, string Password)
    : ICommand<AccessTokenResponse>;