using FluentAssertions;
using Transportathon.Domain.Bookings;

namespace Transportathon.Domain.UnitTests.Bookings;

public class BookingTests
{

    [Fact]
    public void Create_Booking_ReturnStatusStarted()
    {
        var booking = Booking.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), BookingStatus.Started, DateTime.Now,
            Guid.NewGuid(), Guid.NewGuid());

        booking.Status.Should().Be(BookingStatus.Started);
    }
    
    
    [Fact]
    public void Create_Booking_ShouldCompleted()
    {
        var booking = Booking.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), BookingStatus.Started, DateTime.Now,
            Guid.NewGuid(), Guid.NewGuid());

        booking.Complete();
        
        booking.Status.Should().Be(BookingStatus.Completed);
        
        
    }
    
    [Fact]
    public void Create_Booking_ShouldDeliverd()
    {
        var booking = Booking.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), BookingStatus.Started, DateTime.Now,
            Guid.NewGuid(), Guid.NewGuid());

        booking.Deliver();
        
        booking.Status.Should().Be(BookingStatus.Delivered);
        
        
    }
    
    [Fact]
    public void Create_Booking_ShouldCancelByUser()
    {
        var booking = Booking.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), BookingStatus.Started, DateTime.Now,
            Guid.NewGuid(), Guid.NewGuid());

        booking.CancelByUser();
        
        booking.Status.Should().Be(BookingStatus.CancelledByUser);
        
        
    }
    
    [Fact]
    public void Create_Booking_ShouldCancelByCompany()
    {
        var booking = Booking.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), BookingStatus.Started, DateTime.Now,
            Guid.NewGuid(), Guid.NewGuid());

        booking.CancelByCompany();
        
        booking.Status.Should().Be(BookingStatus.CancelledByCompany);
        
        
    }
}