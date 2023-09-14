using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Application.Exceptions;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Transports;
using Transportathon.Domain.Users;

namespace Transportathon.Application.Transports.AnswerTransportRequest;

public class AnswerTransportRequestCommandHandler : ICommandHandler<AnswerTransportRequestCommand>
{
    private readonly ITransportRequestRepository _transportRequestRepository;
    private readonly ITransportRequestAnswerRepository _transportRequestAnswerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AnswerTransportRequestCommandHandler(ITransportRequestRepository transportRequestRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, ITransportRequestAnswerRepository transportRequestAnswerRepository)
    {
        _transportRequestRepository = transportRequestRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _transportRequestAnswerRepository = transportRequestAnswerRepository;
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

        var answer = TransportRequestAnswer.Create(transportRequest, request.Price, user.Company.Id);
        
        await _transportRequestAnswerRepository.AddAsync(answer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}