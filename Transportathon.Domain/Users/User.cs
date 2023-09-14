using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Bookings;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;

namespace Transportathon.Domain.Users;

public sealed class User : Entity
{
    public User(Guid id, Name name, Email email, Phone phone, UserRole role, Company? company) : base(id)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Role = role;
        Company = company;
    }

    private User() { }

    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public UserRole Role { get; private set ; }

    public string Password { get; set; }
    public Company? Company { get; private set; }

    public List<Booking> Bookings { get; private set; } = new();

    public static User Create(Name name, Email email, Phone phone, UserRole role, Company? company = null)
    {
        var user = new User(Guid.NewGuid(), name, email, phone, role, company);


        return user;
    }

    public void AddCompany(Company company)
    {
        Company = company;
    }

    public void AddPassword(string password)
    {
        Password = password;
    }
    
}