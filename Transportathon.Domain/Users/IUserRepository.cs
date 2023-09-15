namespace Transportathon.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task AddAsync(User user);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<User?> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default);
}