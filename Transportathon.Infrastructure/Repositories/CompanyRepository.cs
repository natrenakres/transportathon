using Transportathon.Domain.Transports;

namespace Transportathon.Infrastructure.Repositories;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}