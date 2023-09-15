using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Bookings;
using Transportathon.Domain.Reviews.Events;
using Transportathon.Domain.Transports;
using Transportathon.Domain.Users;

namespace Transportathon.Domain.Reviews;

public sealed class Review : Entity
{
    private Review(
        Guid id,
        Guid requestId,
        Guid bookingId,
        Guid userId,
        Rating rating,
        Comment comment,
        DateTime createdOnUtc)
        : base(id)
    {
        TransportRequestId = requestId;
        BookingId = bookingId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
        CreatedOnUtc = createdOnUtc;
    }

    private Review() { }

    public Guid TransportRequestId { get; private set; }
    public TransportRequest? TransportRequest { get; private set; }

    public Guid BookingId { get; private set; }
    public Booking? Booking { get; private set; }

    public Guid UserId { get; private set; }

    public User? User { get; private set; }

    public Rating Rating { get; private set; }

    public Comment Comment { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public static Result<Review> Create(
        Booking booking,
        Rating rating,
        Comment comment,
        DateTime createdOnUtc)
    {
        if (booking.Status != BookingStatus.Completed)
        {
            return Result.Failure<Review>(ReviewErrors.NotEligible);
        }

        var review = new Review(
            Guid.NewGuid(),
            booking.RequestId,
            booking.Id,
            booking.UserId!.Value,
            rating,
            comment,
            createdOnUtc);

        review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

        return review;
    }
}