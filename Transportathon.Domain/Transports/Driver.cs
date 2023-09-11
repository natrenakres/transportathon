using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Shared;

namespace Transportathon.Domain.Transports;

public sealed class Driver : Entity
{
    public Driver(Guid id, Name name, Experience experience) : base(id)
    {
        Name = name;
        Experience = experience;
    }

    public Name Name { get; private set; }

    public Experience Experience { get; private set; }

    public static Driver Create(Name name, Experience experience)
    {
        var driver = new Driver(Guid.NewGuid(), name, experience);

        return driver;
    }
}