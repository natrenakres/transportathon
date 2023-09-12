using FluentValidation;

namespace Transportathon.Application.Bookings.ReserveBooking;

public class ReserveBookingCommandValidator : AbstractValidator<ReserveBookingCommand>
{
    public ReserveBookingCommandValidator()
    {
        RuleFor(c => c.TransportRequestId).NotEmpty();
        RuleFor(c => c.VehicleId).NotEmpty();
    }
}