using Transportathon.Application.Abstractions.Messaging;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Bookings;

namespace Transportathon.Application.Bookings.CompleteBooking;

internal sealed class CompleteBookingCommandHandler : ICommandHandler<CompleteBookingCommand, BookingStatus>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteBookingCommandHandler(IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BookingStatus>> Handle(CompleteBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);

        if (booking is null)
        {
            return Result.Failure<BookingStatus>(BookingErrors.NotFound);
        }

        if (booking.Status is BookingStatus.CancelledByUser or BookingStatus.CancelledByCompany)
        {
            return Result.Failure<BookingStatus>(BookingErrors.NotEligible);
        }

        booking.Complete();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return booking.Status;
    }
}