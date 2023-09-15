namespace Transportathon.Application.Transports.GetTransportRequestAnswers;

public record TransportRequestAnswersResponse(
     string CompanyName, decimal Amount, string Currency, bool IsAcceptedFromMember);