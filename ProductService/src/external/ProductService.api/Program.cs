using MassTransit;
using ProductService.api.Configurations;
using ProductService.application.Configuration;
using ProductService.infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddProductApplicationServices();
builder.Services.AddProductPresentationService(builder.Configuration);
builder.Services.AddProductInfrastructureServices();
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumers(typeof(Program).Assembly);
    busConfigurator.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]!);
            h.Password(builder.Configuration["MessageBroker:Password"]!);
        });
        config.ConfigureEndpoints(context);
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        // Direct SwaggerUI to use the .NET 10 native OpenAPI file
        options.SwaggerEndpoint("/openapi/v1.json", "Transaction Webhook Api v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
