namespace Transportathon.Api.Controllers.Transports;

public record TransportRequest(DateTime BeginDate, int Type, string Description, TransportRequestAddress Address);

public record AnswerTransportRequest(decimal Amount, string Currency);


public record TransportRequestAddress(
    string Country,
    string State,
    string ZipCode,
    string City,
    string Street);