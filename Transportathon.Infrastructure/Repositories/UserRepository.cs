using Microsoft.EntityFrameworkCore;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Users;

namespace Transportathon.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
            .FirstOrDefaultAsync(user => user.Email == new Email(email), cancellationToken);
    }
}