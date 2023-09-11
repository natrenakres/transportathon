using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Transports;
using Transportathon.Domain.Users;

namespace Transportathon.Application.Transports.AnswerTransportRequest;

public class AnswerTransportRequestCommandHandler : ICommandHandler<AnswerTransportRequestCommand>
{
    private readonly ITransportRequestRepository _transportRequestRepository;
    private readonly IUserRepository _userRepository;

    public AnswerTransportRequestCommandHandler(ITransportRequestRepository transportRequestRepository, IUserRepository userRepository)
    {
        _transportRequestRepository = transportRequestRepository;
        _userRepository = userRepository;
    }


    public async Task<Result> Handle(AnswerTransportRequestCommand request, CancellationToken cancellationToken)
    {
        var transportRequest = await _transportRequestRepository.GetByIdAsync(request.TransportRequestId, cancellationToken);

        if (transportRequest is null)
        {
            return Result.Failure(TransportRequestErrors.NotFound);
        }

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user?.Company is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        var answer = TransportRequestAnswer.Create(transportRequest, request.Price, user.Company!);
    
        return Result.Success();
    }
}