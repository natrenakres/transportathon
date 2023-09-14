using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Application.Transports.GetAllTransportRequest;
using Transportathon.Application.Transports.GetTransportRequest;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Transports.GetAllTransportRequest;

public class GetAllTransportRequestQueryHandler : IQueryHandler<GetAllTransportRequestQuery, List<TransportRequestResponse>>
{
    private readonly ITransportRequestRepository _transportRequestRepository;

    public GetAllTransportRequestQueryHandler(ITransportRequestRepository transportRequestRepository)
    {
        _transportRequestRepository = transportRequestRepository;
    }


    public async Task<Result<List<TransportRequestResponse>>> Handle(GetAllTransportRequestQuery request, CancellationToken cancellationToken)
    {
        var result = await _transportRequestRepository.GetAllAsync(cancellationToken);

        return result.Select(transportRequest => new TransportRequestResponse
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
        }).ToList();
    }
}