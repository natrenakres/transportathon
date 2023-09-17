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
        var memberEmail = new Email("member@mail.com");
        var member = User.Create(
            new Name(faker.Name.FullName()),
            memberEmail,
            new Phone(faker.Phone.PhoneNumber()),
            "1q2w3e4r",
            UserRole.Member);

        var ownerEmail = new Email("owner@mail.com");
        var owner = User.Create(
            new Name(faker.Name.FullName()),
            ownerEmail,
            new Phone(faker.Phone.PhoneNumber()),
            "1q2w3e4r",
            UserRole.Owner);
        var companyEmail = new Email("nakliyeci@mail.com");
        var company = Company.Create(
            new Name(faker.Company.CompanyName()),
            new Logo("company_logo.png"),
            companyEmail,
            new Phone(faker.Phone.PhoneNumber()),
            owner.Id);

        var vehicles = new List<Vehicle>();
        for (int i = 0; i < 5; i++)
        {
            var driverName = new Name($"Driver {i+1}");
            var driver = Driver.Create(driverName, new Experience(2*(i+1)));
            var numberPlate = new NumberPlate($"34-AK-77{i}");
            var vehicle = Vehicle.Create(
                new VehicleModel("Mercedes"), 
                new Year(2010+i), 
                VehicleType.Lorry, 
                numberPlate,
                new Color("white"));
            var carrier1 = Carrier.Create(new Name($"C{i+1}"), new Year(1*(i+1)), Profession.Normal, false);
            var carrier2 = Carrier.Create(new Name($"C{i+1}"), new Year(2*(i+1)), Profession.Normal, false);
            var carrier3 = Carrier.Create(new Name($"C{i+1}"), new Year(3*(i+1)), Profession.Carpenter, false);
            var carrier4 = Carrier.Create(new Name($"C{i+1}"), new Year(4*(i+1)), Profession.Installer, true);
            
            
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
            
            vehicles.Add(vehicle);
        }

        if (await dbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == memberEmail) is null)
        {
            dbContext.Set<User>().Add(member);
            await dbContext.SaveChangesAsync();
        }
        

        if(await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == ownerEmail) is null)
        {
            dbContext.Set<User>().Add(owner);
            dbContext.Set<Company>().Add(company);

            foreach (var vehicle in vehicles)
            {
                company?.AddVehicle(vehicle);
            }
            await dbContext.SaveChangesAsync();
        }
        
        
    }
}