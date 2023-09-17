using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transportathon.Application.Bookings.CompleteBooking;
using Transportathon.Application.Bookings.GetBooking;
using Transportathon.Application.Bookings.GetBookings;
using Transportathon.Application.Bookings.ReserveBooking;

namespace Transportathon.Api.Controllers.Bookings;

[ApiController]
[Route("api/bookings")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly ISender _sender;

    public BookingsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings(CancellationToken cancellationToken)
    {
        var query = new GetBookingsQuery();
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }
    
    [HttpGet("{transportRequestId:guid}")]
    public async Task<IActionResult> GetBooking(Guid transportRequestId, CancellationToken cancellationToken)
    {
        var query = new GetBookingQuery(transportRequestId);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> ReserveBooking(ReserveBookingRequest request, CancellationToken cancellationToken)
    {
        var command = new ReserveBookingCommand(request.TransportRequestId, request.VehicleId);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        

        return CreatedAtAction(nameof(GetBooking), new { id = result.Value }, result.Value);
    }

    [HttpPut("{bookingId:guid}")]
    public async Task<IActionResult> CompleteBooking(Guid bookingId, CancellationToken cancellationToken)
    {
        var command = new CompleteBookingCommand(bookingId);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}