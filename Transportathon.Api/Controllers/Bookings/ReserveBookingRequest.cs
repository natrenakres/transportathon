namespace Transportathon.Api.Controllers.Bookings;

public record ReserveBookingRequest(Guid TransportRequestId, Guid VehicleId);