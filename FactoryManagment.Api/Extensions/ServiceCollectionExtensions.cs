using FactoryManagment.Application;
using FactoryManagment.Infrastructure;

namespace FactoryManagment.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddApplication();
        services.AddInfrastructure(configuration);
        return services;
    }


}
