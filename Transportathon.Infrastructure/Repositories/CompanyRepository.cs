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
    
    public async Task<Company?> GetByIdWidthInfoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Company>()
            .Include(c => c.Vehicles).ThenInclude(v => v.Carriers)
            .Include(c => c.Vehicles).ThenInclude(v => v.Driver)
            .Include(c => c.Bookings).ThenInclude(b => b.Reviews)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}