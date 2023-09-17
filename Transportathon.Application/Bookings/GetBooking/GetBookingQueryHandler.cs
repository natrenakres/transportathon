using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Application.Bookings.GetBookings;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Bookings;

namespace Transportathon.Application.Bookings.GetBooking;

public class GetBookingQueryHandler : IQueryHandler<GetBookingQuery, BookingResponse>
{
    private readonly IBookingRepository _bookingRepository;

    public GetBookingQueryHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByTransportRequestIdAsync(request.TransportRequestId, cancellationToken);

        if (booking is null)
        {
            return Result.Failure<BookingResponse>(BookingErrors.NotFound);
        }

        return new BookingResponse(booking.Id, booking.Status.ToString(), booking.BeginDate, booking.Company?.Name.Value,
            booking.CompanyId, booking.Vehicle?.NumberPlate.Value, booking.VehicleId);
    }
}