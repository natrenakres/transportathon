namespace Transportathon.Application.Transports.GetTransportRequest;

public sealed class TransportRequestResponse
{
    public Guid Id { get; init; }
    public DateTime BeginDate { get; init; }

    public DateTime? EstimatedEndDate { get; init; }

    public string Type { get; init; }
    public string Status { get; init; }

    public bool IsCompleted { get; init; }
    
    public AddressResponse Address { get; init; }

    public decimal? Price { get; init; }

    public string? Currency { get; init; }

    public string Description { get; init; }    
}