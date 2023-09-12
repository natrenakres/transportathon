using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Shared;

namespace Transportathon.Domain.Transports;

public sealed class Carrier : Entity
{
    public Carrier(Guid id, Name name, Year experience, Profession profession, bool canCommunicateWithMember) : base(id)
    {
        Name = name;
        Experience = experience;
        Profession = profession;
        CanCommunicateWithMember = canCommunicateWithMember;
    }

    public Name Name { get;  private set; }

    public bool CanCommunicateWithMember { get; private set; }

    public Year Experience { get; private set; }

    public Profession Profession { get; private set; }

    public static Carrier Create(Name name, Year experience, Profession profession, bool canCommunicateWithMember)
    {
        var carrier = new Carrier(Guid.NewGuid(), name, experience, profession, canCommunicateWithMember);

        return carrier;
    }
}