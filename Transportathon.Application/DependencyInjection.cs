using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Transportathon.Application.Abstractions.Behaviors;

namespace Transportathon.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);

            configuration.AddOpenBehavior(typeof(BaseBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });


        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}