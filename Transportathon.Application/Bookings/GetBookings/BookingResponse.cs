using Transportathon.Domain.Bookings;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.Bookings.GetBookings;

public record BookingResponse(Guid Id, string Status, DateTime BeginDate, string CompanyName,
    Guid CompanyId, string NumberPlate, Guid VehicleId);