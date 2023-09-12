using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Bookings;

namespace Transportathon.Application.Bookings.CompleteBooking;

public record CompleteBookingCommand(Guid BookingId) : ICommand<BookingStatus>;