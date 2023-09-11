namespace Transportathon.Domain.Bookings;

public enum BookingStatus
{
    Started = 1,
    Confirmed = 2,
    CancelledByCompany = 3,
    CancelledByUser = 4,
    Delivered = 5,
    Completed = 6
}