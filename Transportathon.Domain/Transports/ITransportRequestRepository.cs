namespace Transportathon.Domain.Transports;

public interface ITransportRequestRepository
{
    Task<TransportRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TransportRequest>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(TransportRequest transportRequest);
}