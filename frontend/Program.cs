using frontend.Application.Configurations;
using frontend.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();

var app = builder.Build();

app.ConfigureApp(app.Environment);

app.Run();
