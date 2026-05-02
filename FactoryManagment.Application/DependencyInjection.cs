using FactoryManagment.Application.Abstractions.Services;
using FactoryManagment.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryManagment.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);

        return services;
    }
}

