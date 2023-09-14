using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Transports.GetTransportRequest;

public class GetTransportRequestQueryHandler : IQueryHandler<GetTransportRequestQuery, TransportRequestResponse>
{
    private readonly ITransportRequestRepository _transportRequestRepository;

    public GetTransportRequestQueryHandler(ITransportRequestRepository transportRequestRepository)
    {
        _transportRequestRepository = transportRequestRepository;
    }

    public async Task<Result<TransportRequestResponse>> Handle(GetTransportRequestQuery request, CancellationToken cancellationToken)
    {
        var transportRequest = await _transportRequestRepository.GetByIdAsync(request.RequestId, cancellationToken);

        if (transportRequest is null)
        {
            return Result.Failure<TransportRequestResponse>(TransportRequestErrors.NotFound);
        }


        var response = new TransportRequestResponse()
        {
            Status = transportRequest.Status.ToString(),
            Type = transportRequest.Type.ToString(),
            Description = transportRequest.Description.Value,
            Currency = transportRequest.Price?.Currency.Code,
            Price = transportRequest.Price?.Amount,
            BeginDate = transportRequest.BeginDate,
            IsCompleted = transportRequest.IsCompleted,
            EstimatedEndDate = transportRequest.EstimatedEndDate,
            Id = transportRequest.Id,
            Address = new AddressResponse
                        {
                            Country = transportRequest.Address.Country,
                            State = transportRequest.Address.State,
                            ZipCode = transportRequest.Address.ZipCode,
                            City = transportRequest.Address.City,
                            Street = transportRequest.Address.Street
                        }
        };

        return response;
    }
}