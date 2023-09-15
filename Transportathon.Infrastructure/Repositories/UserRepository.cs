using Microsoft.EntityFrameworkCore;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Users;

namespace Transportathon.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<User>()
            .Include(u => u.Company)
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }
    
    public async Task<User?> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<User>()
            .Include(u => u.Company).ThenInclude(c => c!.Vehicles)
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
            .FirstOrDefaultAsync(user => user.Email == new Email(email), cancellationToken);
    }
}