using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;

namespace Transportathon.Domain.Users;

public sealed class User : Entity
{
    public User(Guid id) : base(id)
    {
        
    }

    public Name Name { get; private set; }

    public Email Email { get; private set; }

    public Phone Phone { get; private set; }

    public UserRole Role { get; private set ; }

    public Company? Company { get; private set; }
    
}