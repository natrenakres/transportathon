using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Application.Transports.GetTransportRequest;

namespace Transportathon.Application.Transports.GetAllTransportRequest;

public record GetAllTransportRequestQuery() : IQuery<List<TransportRequestResponse>>;