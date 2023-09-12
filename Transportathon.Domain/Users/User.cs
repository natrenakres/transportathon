using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;

namespace Transportathon.Domain.Users;

public sealed class User : Entity
{
    public User(Guid id, Name name, Email email, Phone phone, UserRole role, Guid? companyId) : base(id)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Role = role;
        CompanyId = companyId;
    }

    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public UserRole Role { get; private set ; }

    public Guid? CompanyId { get; private set; }
    public Company? Company { get; private set; }

    public static User Create(Name name, Email email, Phone phone, UserRole role, Guid? companyId)
    {
        var user = new User(Guid.NewGuid(), name, email, phone, role, companyId);


        return user;
    }

    public void AddCompany(Company company)
    {
        CompanyId = company.Id;
    }
    
}