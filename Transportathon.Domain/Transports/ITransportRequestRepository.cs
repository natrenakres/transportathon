namespace Transportathon.Domain.Transports;

public interface ITransportRequestRepository
{
    Task<TransportRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TransportRequest>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<TransportRequest>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<TransportRequest?> GetWithAnswersAsync(Guid requestId, CancellationToken cancellationToken = default);
    Task<TransportRequest?> GetByUserIdAsync(Guid requestId, Guid userId,
        CancellationToken cancellationToken = default);

    Task AddAsync(TransportRequest transportRequest);
}