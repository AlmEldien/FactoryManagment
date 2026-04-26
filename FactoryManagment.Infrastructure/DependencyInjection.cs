using FactoryManagment.Application.Interfaces;
using FactoryManagment.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryManagment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // internal class — only accessible through the interface
            services.AddScoped<IProductionRepository, ProductionRepository>();

            return services;
        }
    }
}
