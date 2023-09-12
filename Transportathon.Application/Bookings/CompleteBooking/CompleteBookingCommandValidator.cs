using FluentValidation;

namespace Transportathon.Application.Bookings.CompleteBooking;

public class CompleteBookingCommandValidator : AbstractValidator<CompleteBookingCommand>
{
    public CompleteBookingCommandValidator()
    {
        RuleFor(c => c.BookingId).NotEmpty();
    }
}