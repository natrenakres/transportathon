namespace Transportathon.Domain.Transports;

public interface ITransportRequestAnswerRepository
{
    Task<TransportRequestAnswer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(TransportRequestAnswer transportRequestAnswer);
}