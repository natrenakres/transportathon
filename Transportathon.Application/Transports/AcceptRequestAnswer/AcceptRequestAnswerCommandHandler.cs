using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Transports.AcceptRequestAnswer;

internal sealed class AcceptRequestAnswerCommandHandler : ICommandHandler<AcceptRequestAnswerCommand, TransportRequestStatus>
{
    private readonly ITransportRequestRepository _transportRequestRepository;
    private readonly ITransportRequestAnswerRepository _transportRequestAnswerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptRequestAnswerCommandHandler(ITransportRequestRepository transportRequestRepository, ITransportRequestAnswerRepository transportRequestAnswerRepository, IUnitOfWork unitOfWork)
    {
        _transportRequestRepository = transportRequestRepository;
        _transportRequestAnswerRepository = transportRequestAnswerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TransportRequestStatus>> Handle(AcceptRequestAnswerCommand request, CancellationToken cancellationToken)
    {
        var transportRequest = await _transportRequestRepository.GetByIdAsync(request.TransportRequestId, cancellationToken);

        if (transportRequest is null)
        {
            return Result.Failure<TransportRequestStatus>(TransportRequestErrors.NotFound);
        }

        var answer =
            await _transportRequestAnswerRepository.GetByIdAsync(request.TransportRequestAnswerId, cancellationToken);

        if (answer is null)
        {
            return Result.Failure<TransportRequestStatus>(TransportRequestAnswerErrors.NotFound);
        }

        transportRequest.Accept(request.TransportRequestAnswerId, answer.Price);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return transportRequest.Status;
    }
}