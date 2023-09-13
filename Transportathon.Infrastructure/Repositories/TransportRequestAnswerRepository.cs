using Transportathon.Domain.Transports;

namespace Transportathon.Infrastructure.Repositories;

public class TransportRequestAnswerRepository : Repository<TransportRequestAnswer>, ITransportRequestAnswerRepository
{
    public TransportRequestAnswerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}