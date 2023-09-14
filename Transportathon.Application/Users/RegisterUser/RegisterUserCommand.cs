using Transportathon.Application.Abstractions.Messaging;

namespace Transportathon.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
        string Email,
        string Name,
        string Phone,
        string Password) : ICommand<Guid>;