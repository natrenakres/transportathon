using Transportathon.Domain.Abstractions;

namespace Transportathon.Domain.Transports;

public sealed class Vehicle : Entity
{
    public Vehicle(Guid id, VehicleModel model, Year year, NumberPlate numberPlate, Color color): base(id)
    {
        Model = model;
        Year = year;
        NumberPlate = numberPlate;
        Color = color;
    }

    public VehicleModel Model { get; private set; }

    public Year Year { get; private set; }

    public VehicleType Type { get; private set; }

    public Guid DriverId { get; private set; }

    public NumberPlate NumberPlate { get; private set; }

    public Color Color { get; private set; }

    public List<Carrier> Carriers { get; private set; } = new();

    public static Vehicle Create(VehicleModel model, Year year, VehicleType type, Guid driverId, NumberPlate numberPlate, Color color)
    {
        var vehicle = new Vehicle(Guid.NewGuid(), model, year, numberPlate, color);

        return vehicle;
    }

    public Vehicle AddCarrier(Carrier carrier)
    {
        Carriers.Add(carrier);
        return this;
    }
}