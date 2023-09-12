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
        
        var user = User.Create("Ali", new Email("s@s.com"), new Phone("555650252"), UserRole.Owner, )
        
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(u => u.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        var handle = new AnswerTransportRequestCommandHandler(transportRepositoryMock.Object,
            userRepositoryMock.Object, new Mock<IUnitOfWork>().Object,
            new Mock<ITransportRequestAnswerRepository>().Object);

        var response = await handle.Handle(command, default);

        response.Error.Should().Be(UserErrors.NotFound);
    }
}