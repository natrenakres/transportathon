using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Users;

namespace Transportathon.Domain.Transports;

public sealed class Company : Entity
{

    public Company(Guid id, Name name, Logo logo, Email email, Phone phone, Guid userId) : base(id)
    {
        Name = name;
        Logo = logo;
        Email = email;
        Phone = phone;
        UserId = userId;
    }

    public Name Name { get; private set; }

    public Logo Logo { get; private set; }

    public Email Email { get; private set; }

    public Phone Phone { get; private set; }

    public Guid UserId { get; private set; }

    public List<Vehicle> Vehicles { get; private set; } = new();


    public static Company Create(Name name, Logo logo, Email email, Phone phone, Guid userId)
    {
        var company = new Company(Guid.NewGuid(), name, logo, email, phone, userId);

        return company;
    }

    public Company AddVehicle(Vehicle vehicle)
    {
        Vehicles.Add(vehicle);
        return this;
    }
}