using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Transports.AddTransportRequest;

public class AddTransportRequestCommandHandler : ICommandHandler<AddTransportRequestCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddTransportRequestCommand request, CancellationToken cancellationToken)
    {
        var transport = TransportRequest.Create(request.Description, request.BeginDate, request.Type, request.Address);

        return transport.Id;
    }
}