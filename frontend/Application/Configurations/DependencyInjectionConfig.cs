namespace frontend.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddHttpClient("API", client =>
                                {
                                    client.BaseAddress = new Uri("http://localhost:5237");
                                });

        return services;
    }
}