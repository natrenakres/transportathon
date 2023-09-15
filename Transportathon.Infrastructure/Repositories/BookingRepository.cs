using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Transportathon.Application.Bookings.GetBookings;
using Transportathon.Domain.Bookings;

namespace Transportathon.Infrastructure.Repositories;

public class BookingRepository : Repository<Booking>, IBookingRepository
{
    public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Booking>> GetAllByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Booking>()
            .Include(b => b.Vehicle)
            .Include(b => b.Company)
            .Where(b => b.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<List<Booking>> GetAllByCompanyId(Guid companyId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Booking>()
            .Include(b => b.Vehicle)
            .Include(b => b.Company)
            .Where(b => b.CompanyId == companyId).ToListAsync(cancellationToken);
    }
}
