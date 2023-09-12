using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Transports.AcceptRequestAnswer;

public record AcceptRequestAnswerCommand(Guid TransportRequestAnswerId, Guid TransportRequestId)
: ICommand<TransportRequestStatus>;