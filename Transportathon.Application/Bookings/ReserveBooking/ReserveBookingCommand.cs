using Transportathon.Application.Abstractions.Messaging;

namespace Transportathon.Application.Bookings.ReserveBooking;

public record ReserveBookingCommand(Guid TransportRequestId, Guid VehicleId) : ICommand;