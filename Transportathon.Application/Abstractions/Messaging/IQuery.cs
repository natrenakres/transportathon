using MediatR;
using Transportathon.Domain.Abstractions;

namespace Transportathon.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IBaseMessage
{
}