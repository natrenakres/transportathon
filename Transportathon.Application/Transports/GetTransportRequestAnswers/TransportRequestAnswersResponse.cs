namespace Transportathon.Application.Transports.GetTransportRequestAnswers;

public record TransportRequestAnswersResponse(Guid Id, 
     string CompanyName,
     Guid CompanyId,
     decimal Amount, string Currency, bool IsAcceptedFromMember);