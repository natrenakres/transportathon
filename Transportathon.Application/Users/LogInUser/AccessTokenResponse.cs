namespace Transportathon.Application.Users.LogInUser;

public sealed record AccessTokenResponse(string AccessToken, string Name, bool IsOwner);