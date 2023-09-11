using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;
using FluentAssertions;

namespace Transportathon.Domain.UnitTests.Transports;

public class TransportRequestTests : BaseTest
{
    private readonly Company _company;

    public TransportRequestTests()
    {
        _company = Company.Create(new Name("1&1 Tasima"), new Logo("logo.png"), 
            new Email("1-1-tasima@email.com"), new Phone("01215552575"), Guid.NewGuid());
        var driver = Driver.Create(new Name("Ali Veli"), new Experience(10));
        var vehicle = Vehicle.Create(new VehicleModel("Mercedes"), new Year(2020), VehicleType.Lorry, driver.Id,
            new NumberPlate("34-AK-775"),
            new Color("white"));

        var carrier1 = Carrier.Create(new Name("C1"), new Year(1), Profession.Normal, false);
        var carrier2 = Carrier.Create(new Name("C2"), new Year(2), Profession.Normal, false);
        var carrier3 = Carrier.Create(new Name("C3"), new Year(3), Profession.Carpenter, false);
        var carrier4 = Carrier.Create(new Name("C4"), new Year(4), Profession.Installer, true);

        vehicle.AddCarrier(carrier1);
        vehicle.AddCarrier(carrier2);
        vehicle.AddCarrier(carrier3);
        vehicle.AddCarrier(carrier4);

        _company.AddVehicle(vehicle);
    }

    [Fact]
    public void Create_TransportRequest_StatusNew()
    {
        var description = new Description("1+1 evimizi Ocak ayinda Muglaya tasimak istiyoruz.");
        var address = new Address("Turkey", "", "48000", "Mugla", "Cumhuruyet");
        var beginDate = DateTime.Parse("01-01-2023 07:00");

        var transportRequest = TransportRequest.Create(description, beginDate, TransportRequestType.HomeToHome, address);

        transportRequest.Status.Should().Be(TransportRequestStatus.New);
    }

    [Fact]
    public void Create_TransportRequestAnswer_ReturnNotNull()
    {
        
        
        var description = new Description("1+1 evimizi Ocak ayinda Muglaya tasimak istiyoruz.");
        var address = new Address("Turkey", "", "48000", "Mugla", "Cumhuruyet");
        var beginDate = DateTime.Parse("01-01-2023 07:00");

        var transportRequest = TransportRequest.Create(description, beginDate, TransportRequestType.HomeToHome, address);
        
        var price = new Money(100, Currency.Tl);
        var answer = TransportRequestAnswer.Create(transportRequest, price, _company);


        transportRequest.Answers.Count.Should().BeGreaterThan(0);
        answer.RequestId.Should().Be(transportRequest.Id);
        answer.Price.Should().Be(price);
    }
    
    [Fact]
    public void Accept_TransportRequestAnswer_ReturnNotNull()
    {
        var description = new Description("1+1 evimizi Ocak ayinda Muglaya tasimak istiyoruz.");
        var address = new Address("Turkey", "", "48000", "Mugla", "Cumhuruyet");
        var beginDate = DateTime.Parse("01-01-2023 07:00");

        var transportRequest = TransportRequest.Create(description, beginDate, TransportRequestType.HomeToHome, address);
        
        var price1 = new Money(100, Currency.Tl);
        var answer1 = TransportRequestAnswer.Create(transportRequest, price1, _company);

        var price2 = new Money(120, Currency.Tl);
        TransportRequestAnswer.Create(transportRequest, price2, _company);


        transportRequest.Accept(answer1.Id, price1);

        transportRequest.Status.Should().Be(TransportRequestStatus.Accepted);
        answer1.IsAcceptedFromMember.Should().BeTrue();
        transportRequest.Price.Should().Be(price1);

    }
    
    
    [Fact]
    public void Get_TransportRequestDriverInfo_ReturnNotNull()
    {
        var description = new Description("1+1 evimizi Ocak ayinda Muglaya tasimak istiyoruz.");
        var address = new Address("Turkey", "", "48000", "Mugla", "Cumhuruyet");
        var beginDate = DateTime.Parse("01-01-2023 07:00");

        var transportRequest = TransportRequest.Create(description, beginDate, TransportRequestType.HomeToHome, address);
        
        var price1 = new Money(100, Currency.Tl);
        var answer1 = TransportRequestAnswer.Create(transportRequest, price1, _company);

        var price2 = new Money(120, Currency.Tl);
        TransportRequestAnswer.Create(transportRequest, price2, _company);
        transportRequest.Accept(answer1.Id, price1);


    }
}