using backend_api.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
            .AddDbServices(builder.Configuration)
            .AddServices();

var app = builder.Build();
app.ConfigureApp(app.Environment);

app.Run();