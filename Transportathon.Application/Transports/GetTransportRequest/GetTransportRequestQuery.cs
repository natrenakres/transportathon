using Transportathon.Application.Abstractions.Messaging;

namespace Transportathon.Application.Transports.GetTransportRequest;

public record GetTransportRequestQuery(Guid RequestId) : IQuery<TransportRequestResponse>;