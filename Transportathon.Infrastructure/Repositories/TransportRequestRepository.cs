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

    public async Task<TransportRequest?> GetByUserIdAsync(Guid requestId, Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TransportRequest>()
            .Include(r => r.Answers).ThenInclude(a => a.Company)
            .FirstOrDefaultAsync(r => (r.UserId == userId &&
                                       r.Id == requestId), cancellationToken);
    }
    
    public async Task<List<TransportRequest>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TransportRequest>()
            .Include(r => r.Answers).ThenInclude(a => a.Company)
            .Where(r => r.UserId == userId)
            .ToListAsync(cancellationToken);

    }
}