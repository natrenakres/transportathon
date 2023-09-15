using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Transports.AddTransportRequest;

internal sealed class AddTransportRequestCommandHandler : ICommandHandler<AddTransportRequestCommand, Guid>
{
    private readonly ITransportRequestRepository _transportRequestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddTransportRequestCommandHandler(ITransportRequestRepository transportRequestRepository, IUnitOfWork unitOfWork)
    {
        _transportRequestRepository = transportRequestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddTransportRequestCommand request, CancellationToken cancellationToken)
    {
        var transportRequest = TransportRequest.Create(request.Description, request.BeginDate, request.Type, request.Address);

        transportRequest.AddUser(request.UserId);
        
        await _transportRequestRepository.AddAsync(transportRequest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return transportRequest.Id;
    }
}