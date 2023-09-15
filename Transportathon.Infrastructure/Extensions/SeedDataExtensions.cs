using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Transportathon.Domain.Shared;
using Transportathon.Domain.Transports;
using Transportathon.Domain.Users;

namespace Transportathon.Infrastructure.Extensions;

public static class SeedDataExtensions
{
    public static async void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var faker = new Faker();


        var email = new Email("owner@mail.com");
        var owner = User.Create(
            new Name(faker.Name.FullName()),
            email,
            new Phone(faker.Phone.PhoneNumber()),
            "1q2w3e4r",
            UserRole.Owner);
        var company1 = Company.Create(
            new Name(faker.Company.CompanyName()),
            new Logo("company_logo.png"),
            new Email("nakliyeci@mail.com"),
            new Phone(faker.Phone.PhoneNumber()),
            owner.Id);

        

        if(await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email) is null)
        {
            dbContext.Set<User>().Add(owner);
            dbContext.Set<Company>().Add(company1);
            await dbContext.SaveChangesAsync();
        }
        
    }
}