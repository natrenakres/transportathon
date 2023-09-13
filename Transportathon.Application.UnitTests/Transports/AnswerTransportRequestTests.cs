using FluentAssertions;
using Moq;
using Transportathon.Application.Transports.AnswerTransportRequest;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;
using Transportathon.Domain.Users;

namespace Transportathon.Application.UnitTests.Transports;

public class AnswerTransportRequestTests
{
    private readonly Company _company;
    private readonly Vehicle _vehicle;

    public AnswerTransportRequestTests()
    {
        _company = Company.Create(new Name("1&1 Tasima"), new Logo("logo.png"), 
            new Email("1-1-tasima@email.com"), new Phone("01215552575"), Guid.NewGuid());
        var driver = Driver.Create(new Name("Ali Veli"), new Experience(10));
        _vehicle = Vehicle.Create(new VehicleModel("Mercedes"), new Year(2020), VehicleType.Lorry, driver.Id,
            new NumberPlate("34-AK-775"),
            new Color("white"));

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
    public async Task Handle_ShouldReturnError_WhenRequestNull()
    {
        var transportRepositoryMock = new Mock<ITransportRequestRepository>();
        transportRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((TransportRequest?)null);
        var price = new Money(100, Currency.Tl);
        var command = new AnswerTransportRequestCommand(Guid.NewGuid(), price, Guid.NewGuid());

        var handle = new AnswerTransportRequestCommandHandler(transportRepositoryMock.Object,
            new Mock<IUserRepository>().Object, new Mock<IUnitOfWork>().Object,
            new Mock<ITransportRequestAnswerRepository>().Object);

        var response = await handle.Handle(command, default);


        response.IsFailure.Should().Be(true);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserIsNull()
    {
        var description = new Description("1+1 evimizi Ocak ayinda Muglaya tasimak istiyoruz.");
        var address = new Address("Turkey", "", "48000", "Mugla", "Cumhuruyet");
        var beginDate = DateTime.Parse("01-01-2023 07:00");

        var transportRequest = TransportRequest.Create(description, beginDate, TransportRequestType.HomeToHome, address);
        
        var transportRepositoryMock = new Mock<ITransportRequestRepository>();
        transportRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(transportRequest);
        var price = new Money(100, Currency.Tl);
        var command = new AnswerTransportRequestCommand(Guid.NewGuid(), price, Guid.NewGuid());
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        var handle = new AnswerTransportRequestCommandHandler(transportRepositoryMock.Object,
            userRepositoryMock.Object, new Mock<IUnitOfWork>().Object,
            new Mock<ITransportRequestAnswerRepository>().Object);

        var response = await handle.Handle(command, default);

        response.Error.Should().Be(UserErrors.NotFound);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserIsNotNullButHasNoCompany()
    {
        var description = new Description("1+1 evimizi Ocak ayinda Muglaya tasimak istiyoruz.");
        var address = new Address("Turkey", "", "48000", "Mugla", "Cumhuruyet");
        var beginDate = DateTime.Parse("01-01-2023 07:00");

        var transportRequest = TransportRequest.Create(description, beginDate, TransportRequestType.HomeToHome, address);
        
        var transportRepositoryMock = new Mock<ITransportRequestRepository>();
        transportRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(transportRequest);
        var price = new Money(100, Currency.Tl);
        var command = new AnswerTransportRequestCommand(Guid.NewGuid(), price, Guid.NewGuid());

        var user = User.Create(new Name("Ali"), new Email("s@s.com"), new Phone("555650252"), UserRole.Owner, null);
        
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handle = new AnswerTransportRequestCommandHandler(transportRepositoryMock.Object,
            userRepositoryMock.Object, new Mock<IUnitOfWork>().Object,
            new Mock<ITransportRequestAnswerRepository>().Object);

        var response = await handle.Handle(command, default);

        response.Error.Should().Be(UserErrors.NotFound);
    }
    
    [Fact]
    public async Task Handle_ShouldSuccess_WhenUserCanAnswerTheRequest()
    {
        var description = new Description("1+1 evimizi Ocak ayinda Muglaya tasimak istiyoruz.");
        var address = new Address("Turkey", "", "48000", "Mugla", "Cumhuruyet");
        var beginDate = DateTime.Parse("01-01-2023 07:00");

        var transportRequest = TransportRequest.Create(description, beginDate, TransportRequestType.HomeToHome, address);
        
        var transportRepositoryMock = new Mock<ITransportRequestRepository>();
        transportRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(transportRequest);
        var price = new Money(100, Currency.Tl);
        var command = new AnswerTransportRequestCommand(Guid.NewGuid(), price, Guid.NewGuid());

        var user = User.Create(new Name("Ali"), new Email("s@s.com"), new Phone("555650252"), UserRole.Owner, _company.Id);
        
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handle = new AnswerTransportRequestCommandHandler(transportRepositoryMock.Object,
            userRepositoryMock.Object, new Mock<IUnitOfWork>().Object,
            new Mock<ITransportRequestAnswerRepository>().Object);

        var response = await handle.Handle(command, default);

        response.IsSuccess.Should().Be(true);
    }
}