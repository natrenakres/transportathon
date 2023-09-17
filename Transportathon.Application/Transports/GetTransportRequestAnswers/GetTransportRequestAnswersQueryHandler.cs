using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Transports.GetTransportRequestAnswers;

public class GetTransportRequestAnswersQueryHandler : IQueryHandler<GetTransportRequestAnswersQuery, List<TransportRequestAnswersResponse>>
{
    private readonly ITransportRequestRepository _transportRequestRepository;

    public GetTransportRequestAnswersQueryHandler(ITransportRequestRepository transportRequestRepository)
    {
        _transportRequestRepository = transportRequestRepository;
    }


    public async Task<Result<List<TransportRequestAnswersResponse>>> Handle(GetTransportRequestAnswersQuery request, CancellationToken cancellationToken)
    {
        var transportRequest = await _transportRequestRepository.GetByUserIdAsync(request.RequestId, request.UserId, cancellationToken);
        
        if (transportRequest is null)
        {
            return Result.Failure<List<TransportRequestAnswersResponse>>(TransportRequestErrors.NotFound);
        }

        var answers = transportRequest.Answers
            .Select(a => new TransportRequestAnswersResponse(a.Id, a.Company.Name.Value, a.CompanyId, a.Price.Amount, a.Price.Currency.Code, a.IsAcceptedFromMember))
            .ToList();

        return answers;
    }
}