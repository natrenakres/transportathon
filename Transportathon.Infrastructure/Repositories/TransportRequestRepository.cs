using Transportathon.Domain.Transports;

namespace Transportathon.Infrastructure.Repositories;

public class TransportRequestRepository : Repository<TransportRequest>, ITransportRequestRepository
{
    public TransportRequestRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}