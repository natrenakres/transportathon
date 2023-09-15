namespace Transportathon.Application.Abstractions.Messaging;

public record BaseRequest
{
    public Guid UserId { get; set; }
    public bool IsOwner { get; set; }
}