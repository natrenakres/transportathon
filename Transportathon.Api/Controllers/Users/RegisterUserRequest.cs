namespace Transportathon.Api.Controllers.Users;

public sealed record RegisterUserRequest(
    string Email,
    string Name,
    string Phone,
    string Password);