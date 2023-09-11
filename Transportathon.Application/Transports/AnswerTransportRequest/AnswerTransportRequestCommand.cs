using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Shared;

namespace Transportathon.Application.Transports.AnswerTransportRequest;

public record AnswerTransportRequestCommand(Guid TransportRequestId, Money Price, Guid UserId) : ICommand;