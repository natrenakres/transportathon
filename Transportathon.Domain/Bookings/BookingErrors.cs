using Transportathon.Domain.Abstractions;

namespace Transportathon.Domain.Bookings;

public static class BookingErrors
{
    public static readonly Error NotFound = new("Booking.Found", "The booking was not found");
    public static readonly Error NotEligible = new(Code: "Booking.Status", "The booking status is not eligible for complete");
}