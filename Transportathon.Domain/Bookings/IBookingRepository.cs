namespace Transportathon.Domain.Bookings;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Booking booking);
    Task<List<Booking>> GetAllByUserId(Guid userId, CancellationToken cancellationToken);
    Task<List<Booking>> GetAllByCompanyId(Guid companyId, CancellationToken cancellationToken);
}