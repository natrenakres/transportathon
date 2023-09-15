using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;

namespace Transportathon.Application.Transports.GetTransportRequestAnswers;

public record GetTransportRequestAnswersQuery(Guid RequestId) : BaseRequest, IQuery<List<TransportRequestAnswersResponse>>;