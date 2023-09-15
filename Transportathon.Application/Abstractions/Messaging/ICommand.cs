using MediatR;
using Transportathon.Domain.Abstractions;

namespace Transportathon.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseMessage
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseMessage
{
}