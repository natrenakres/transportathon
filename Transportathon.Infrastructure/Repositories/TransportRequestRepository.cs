using Microsoft.EntityFrameworkCore;
using Transportathon.Domain.Transports;

namespace Transportathon.Infrastructure.Repositories;

public class TransportRequestRepository : Repository<TransportRequest>, ITransportRequestRepository
{
    public TransportRequestRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<TransportRequest>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext
            .Set<TransportRequest>()
            .ToListAsync(cancellationToken);
    }
}