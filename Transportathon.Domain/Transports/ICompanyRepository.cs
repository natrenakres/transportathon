namespace Transportathon.Domain.Transports;

public interface ICompanyRepository
{
    Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Company?> GetByIdWidthInfoAsync(Guid id, CancellationToken cancellationToken = default);

}