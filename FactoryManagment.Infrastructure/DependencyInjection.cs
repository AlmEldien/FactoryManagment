using FactoryManagment.Application.Abstractions.Repositories;
using FactoryManagment.Application.Abstractions.Services;
using FactoryManagment.Application.Services;
using FactoryManagment.Infrastructure.Persistence;
using FactoryManagment.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryManagment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                {
                    var origins = configuration.GetSection("AllowedOrigins").Get<string[]>();
                    if (origins != null && origins.Length > 0)
                        builder.WithOrigins(origins);
                    else
                        builder.AllowAnyOrigin();

                    builder.AllowAnyMethod().AllowAnyHeader();
                })
             );

            services.AddDbContext<ApplicationDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

            // Repositories
            services.AddScoped<IAlertRepository, AlertRepository>();

            // Services
            services.AddScoped<IAlertService, AlertService>();

            return services;
        }
    }
}

