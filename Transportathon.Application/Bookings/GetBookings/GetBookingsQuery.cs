using Transportathon.Application.Abstractions.Messaging;

namespace Transportathon.Application.Bookings.GetBookings;

public record GetBookingsQuery() : BaseRequest, IQuery<List<BookingResponse>>;