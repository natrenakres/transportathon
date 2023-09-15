using Microsoft.EntityFrameworkCore;
using Transportathon.Domain.Abstractions;

namespace Transportathon.Infrastructure.Repositories;

public abstract class Repository<T>
    where T : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task AddAsync(T entity)
    {
        await DbContext.AddAsync(entity);
    }
}