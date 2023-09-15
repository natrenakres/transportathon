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
        var companyEmail = new Email("nakliyeci@mail.com");
        var company1 = Company.Create(
            new Name(faker.Company.CompanyName()),
            new Logo("company_logo.png"),
            companyEmail,
            new Phone(faker.Phone.PhoneNumber()),
            owner.Id);

        var driverName = new Name("Driver 1");
        var driver = Driver.Create(driverName, new Experience(10));
        var numberPlate = new NumberPlate("34-AK-775");
        var vehicle = Vehicle.Create(
            new VehicleModel("Mercedes"), 
            new Year(2020), 
            VehicleType.Lorry, 
            numberPlate,
            new Color("white"));
        var carrier1 = Carrier.Create(new Name("C1"), new Year(1), Profession.Normal, false);
        var carrier2 = Carrier.Create(new Name("C2"), new Year(2), Profession.Normal, false);
        var carrier3 = Carrier.Create(new Name("C3"), new Year(3), Profession.Carpenter, false);
        var carrier4 = Carrier.Create(new Name("C4"), new Year(4), Profession.Installer, true);
        
        if(await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email) is null)
        {
            dbContext.Set<User>().Add(owner);
            dbContext.Set<Company>().Add(company1);
            await dbContext.SaveChangesAsync();
        }

        if (await dbContext.Set<Vehicle>().FirstOrDefaultAsync(v => v.NumberPlate == numberPlate) is null)
        {
            var company = await dbContext.Set<Company>().FirstOrDefaultAsync(x => x.Email == companyEmail);
            dbContext.Set<Driver>().Add(driver);
            dbContext.Set<Carrier>().Add(carrier1);
            dbContext.Set<Carrier>().Add(carrier2);
            dbContext.Set<Carrier>().Add(carrier3);
            dbContext.Set<Carrier>().Add(carrier4);
            dbContext.Set<Vehicle>().Add(vehicle);
            
            vehicle.AddDriver(driver);
            vehicle.AddCarrier(carrier1);
            vehicle.AddCarrier(carrier2);
            vehicle.AddCarrier(carrier3);
            vehicle.AddCarrier(carrier4);
            company?.AddVehicle(vehicle);
            await dbContext.SaveChangesAsync();
        }
        
    }
}