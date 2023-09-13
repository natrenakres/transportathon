using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Bookings;
using Transportathon.Domain.Transports;
using Transportathon.Domain.Users;
using Transportathon.Infrastructure.Repositories;

namespace Transportathon.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("Database") ??
                               throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ITransportRequestRepository, TransportRequestRepository>();
        services.AddScoped<ITransportRequestAnswerRepository, TransportRequestAnswerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        


        return services;
    }
}