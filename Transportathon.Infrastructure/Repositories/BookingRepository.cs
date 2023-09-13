using Transportathon.Domain.Bookings;

namespace Transportathon.Infrastructure.Repositories;

public class BookingRepository : Repository<Booking>, IBookingRepository
{
    public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
