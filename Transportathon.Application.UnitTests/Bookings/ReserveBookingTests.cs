using FluentAssertions;
using Moq;
using Transportathon.Application.Bookings.CompleteBooking;
using Transportathon.Application.Bookings.ReserveBooking;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Bookings;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;

namespace Transportathon.Application.UnitTests.Bookings;

public class ReserveBookingTests
{
    private readonly Company _company;
    private readonly Vehicle _vehicle;

    public ReserveBookingTests()
    {
        _company = Company.Create(new Name("1&1 Tasima"), new Logo("logo.png"), 
            new Email("1-1-tasima@email.com"), new Phone("01215552575"), Guid.NewGuid());
        var driver = Driver.Create(new Name("Ali Veli"), new Experience(10));
        _vehicle = Vehicle.Create(new VehicleModel("Mercedes"), new Year(2020), VehicleType.Lorry, new NumberPlate("34-AK-775"),
            new Color("white"));
        _vehicle.AddDriver(driver);

        var carrier1 = Carrier.Create(new Name("C1"), new Year(1), Profession.Normal, false);
        var carrier2 = Carrier.Create(new Name("C2"), new Year(2), Profession.Normal, false);
        var carrier3 = Carrier.Create(new Name("C3"), new Year(3), Profession.Carpenter, false);
        var carrier4 = Carrier.Create(new Name("C4"), new Year(4), Profession.Installer, true);

        _vehicle.AddCarrier(carrier1);
        _vehicle.AddCarrier(carrier2);
        _vehicle.AddCarrier(carrier3);
        _vehicle.AddCarrier(carrier4);

        _company.AddVehicle(_vehicle);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenRequestIsNull()
    {
        // Arrange
        var command = new ReserveBookingCommand(Guid.NewGuid(), Guid.NewGuid());
        var transportRepositoryMock = new Mock<ITransportRequestRepository>();
        transportRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((TransportRequest?)null);

        var handler = new ReserveBookingCommandHandler(transportRepositoryMock.Object,
            new Mock<IUnitOfWork>().Object, new Mock<ICompanyRepository>().Object,
            new Mock<IBookingRepository>().Object);
        
        // Act
        var result = await handler.Handle(command, default);
        
        // Assert
        result.Error.Should().Be(TransportRequestErrors.NotFound);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenAnswerIsNull()
    {
        // Arrange
        var description = new Description("1+1 evimizi Ocak ayinda Muglaya tasimak istiyoruz.");
        var address = new Address("Turkey", "", "48000", "Mugla", "Cumhuruyet");
        var beginDate = DateTime.Parse("01-01-2023 07:00");

        var transportRequest = TransportRequest.Create(description, beginDate, TransportRequestType.HomeToHome, address);
        
        var command = new ReserveBookingCommand(Guid.NewGuid(), Guid.NewGuid());
        var transportRepositoryMock = new Mock<ITransportRequestRepository>();
        transportRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(transportRequest);

        var handler = new ReserveBookingCommandHandler(transportRepositoryMock.Object,
            new Mock<IUnitOfWork>().Object, new Mock<ICompanyRepository>().Object,
            new Mock<IBookingRepository>().Object);
        
        // Act
        var result = await handler.Handle(command, default);
        
        // Assert
        result.Error.Should().Be(TransportRequestAnswerErrors.NotFound);
    }
    
    
    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenBookingIsCreated()
    {
        // Arrange
        var description = new Description("1+1 evimizi Ocak ayinda Muglaya tasimak istiyoruz.");
        var address = new Address("Turkey", "", "48000", "Mugla", "Cumhuruyet");
        var beginDate = DateTime.Parse("01-01-2023 07:00");

        var transportRequest = TransportRequest.Create(description, beginDate, TransportRequestType.HomeToHome, address);
        var price = new Money(100, Currency.Tl);
        var answer = TransportRequestAnswer.Create(transportRequest, price, _company.Id);
        
        answer.SetIsAccepted();

        var command = new ReserveBookingCommand(Guid.NewGuid(), _vehicle.Id);
        var transportRepositoryMock = new Mock<ITransportRequestRepository>();
        transportRepositoryMock.Setup(u => 
                u.GetByIdAsync(
                    It.IsAny<Guid>(),It.IsAny<CancellationToken>()))
            .ReturnsAsync(transportRequest);
        var companyRepositoryMock = new Mock<ICompanyRepository>();
        companyRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_company);

        var handler = new ReserveBookingCommandHandler(transportRepositoryMock.Object,
            new Mock<IUnitOfWork>().Object, companyRepositoryMock.Object,
            new Mock<IBookingRepository>().Object);
        
        // Act
        var result = await handler.Handle(command, default);
        
        // Assert
        result.IsSuccess.Should().Be(true);
    }
    
    
    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenBookingIsCompleted()
    {
        var booking = Booking.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), BookingStatus.Started, DateTime.Now,
            Guid.NewGuid(), Guid.NewGuid());
        
        var command = new CompleteBookingCommand(Guid.NewGuid());

        var bookingRepositoryMock = new Mock<IBookingRepository>();
        bookingRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(booking);

        var handler = new CompleteBookingCommandHandler(bookingRepositoryMock.Object, new Mock<IUnitOfWork>().Object);

        
        var result = await handler.Handle(command, default);

        
        result.Value.Should().Be(BookingStatus.Completed);

    }
}