namespace Transportathon.Application.Transports.GetTransportRequestAnswers;

public record TransportRequestAnswersResponse(Guid Id, 
     string CompanyName, decimal Amount, string Currency, bool IsAcceptedFromMember);