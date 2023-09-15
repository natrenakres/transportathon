using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Bookings;
using Transportathon.Domain.Users;

namespace Transportathon.Application.Bookings.GetBookings;

public class GetBookingsQueryHandler : IQueryHandler<GetBookingsQuery, List<BookingResponse>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;

    public GetBookingsQueryHandler(IBookingRepository bookingRepository, IUserRepository userRepository)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<List<BookingResponse>>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        List<Booking> result;

        if (request.IsOwner)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user?.Company is null)
            {
                return Result.Failure<List<BookingResponse>>(UserErrors.NotFound);
            }
            result = await _bookingRepository.GetAllByCompanyId(user.Company.Id, cancellationToken);
        }
        else
        {
            result = await _bookingRepository.GetAllByUserId(request.UserId, cancellationToken);
        }

        return result.Select(b =>
                new BookingResponse(b.Id, b.Status.ToString(), b.BeginDate, b.Company?.Name.Value!, b.CompanyId, b.Vehicle?.NumberPlate.Value!, b.VehicleId))
            .ToList();
    }
}