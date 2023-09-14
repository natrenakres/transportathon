using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Transportathon.Application.Abstractions.Authentication;
using Transportathon.Domain.Abstractions;
using Transportathon.Domain.Bookings;
using Transportathon.Domain.Transports;
using Transportathon.Domain.Users;
using Transportathon.Infrastructure.Authentication;
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

        services.ConfigureOptions<AuthOptionsSetup>();


        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer((options) =>
            {
                var authOptions = new AuthOptions();
                configuration.Bind(AuthOptions.SectionName, authOptions);
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidAudience = authOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.Secret))
                };

                if (options.Events != null)
                {
                    options.Events.OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("jwt"))
                        {
                            context.Token = context.Request.Cookies["jwt"];
                        }

                        return Task.CompletedTask;
                    };    
                }
                
                
            });

        services.AddScoped<IAuthenticationService, AuthenticationService>();


        return services;
    }
}