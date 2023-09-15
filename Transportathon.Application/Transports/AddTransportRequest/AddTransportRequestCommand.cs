using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Transports.AddTransportRequest;

public record AddTransportRequestCommand(DateTime BeginDate, TransportRequestType Type, Description Description, Address Address) 
: BaseRequest, ICommand<Guid>;