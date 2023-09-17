using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Application.Bookings.GetBookings;

namespace Transportathon.Application.Bookings.GetBooking;

public record GetBookingQuery(Guid TransportRequestId) : IQuery<BookingResponse>;