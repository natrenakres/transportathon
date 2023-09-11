using FluentAssertions;
using Transportathon.Domain.Bookings;
using Transportathon.Domain.Reviews;

namespace Transportathon.Domain.UnitTests.Reviews;

public class ReviewUnitTests
{
    [Fact]
    public void Create_AddNewReviewToBooking()
    {
        var booking = Booking.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), BookingStatus.Started, DateTime.Now,
            Guid.NewGuid(), Guid.NewGuid());

        booking.Complete();

        var rating = Rating.Create(5);

        var review =  Review.Create(booking, rating.Value, new Comment("Harika tasidilar. Hic bir sey kirilmadi"), DateTime.Now);

        review.IsSuccess.Should().Be(true);
    }
    
    [Fact]
    public void Create_AddNewReviewToBooking_Not()
    {
        var booking = Booking.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), BookingStatus.Started, DateTime.Now,
            Guid.NewGuid(), Guid.NewGuid());

        var rating = Rating.Create(5);

        var review =  Review.Create(booking, rating.Value, new Comment("Harika tasidilar. Hic bir sey kirilmadi"), DateTime.Now);

        review.IsSuccess.Should().Be(false);
    }
}