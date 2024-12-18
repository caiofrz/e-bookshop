namespace backend_api.Application.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}