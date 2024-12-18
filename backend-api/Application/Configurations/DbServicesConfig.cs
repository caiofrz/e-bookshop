using backend_api.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Application.Configurations;

public static class DbServicesConfig
{
    public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnection = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(postgresConnection));

        return services;
    }
}