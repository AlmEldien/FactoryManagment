using FactoryManagment.Application.Abstractions.Identity;
using FactoryManagment.Application.Abstractions.Repositories;
using FactoryManagment.Infrastructure.Identity;
using FactoryManagment.Infrastructure.Persistence;
using FactoryManagment.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FactoryManagment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

            // Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                // TODO: who make 'register' end point must confirm email before login
                //options.SignIn.RequireConfirmedEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Add Authentication
            services.AddAuthConfig(configuration);

            // Add Authorization
            services.AddAuthorization();

            return services;
        }

        public static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
                if (jwtSettings == null) throw new InvalidOperationException("JwtSettings is not configured.");
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });

            return services;
        }
    }
}
