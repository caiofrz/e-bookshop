using System.Reflection;
using backend_api.Application.Filters;
using backend_api.Application.Mapper;
using backend_api.Application.Services;
using backend_api.Domain.Interfaces.Repositories;
using backend_api.Domain.Interfaces.Services;
using backend_api.Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Application.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(new ValidateModelAttribute());
        });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddAutoMapper(typeof(DtoToModelMappingProfile));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        
        #region repositories
        services.AddTransient<IBookRepository, BookRepository>();
        #endregion

        #region services
        services.AddTransient<IBookService, BookService>();
        #endregion

        return services;
    }
}