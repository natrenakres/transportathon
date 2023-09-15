using Microsoft.EntityFrameworkCore;
using Transportathon.Domain.Transports;

namespace Transportathon.Infrastructure.Repositories;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Company>()
            .Include(c => c.Vehicles).ThenInclude(v => v.Carriers)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}