using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductService.Infrastructure;
using ProductService.Application.Services;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using ProductService.API;
using ProductService.Infrastructure.context;
using MassTransit;
using ProductService.Infrastructure.Consumers;
using RabbitMQ.Client;
using ProductService.Infrastructure.Cache;
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.Configure<MongoDbSettings>(
    options =>
    {
        options.MongoDbConnectionString = builder.Configuration.GetConnectionString("mongodb");
        options.DatabaseName = "ProductDb";
    }
    );

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.MongoDbConnectionString);
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
    options.ListenAnyIP(5001, listen => listen.Protocols =
    Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2);
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis"); // Use Aspire-injected connection string
});


builder.Services.AddScoped<IProductService,ProductServ>(); 
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<ICloudinaryService,CloudinaryService>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddScoped<MongoDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(x => new Cloudinary(builder.Configuration["Cloudinary:CloudinaryUrl"]));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AddOrderConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("rabbitmq"), "/", h => // Use Aspire-injected connection string
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);

        cfg.ReceiveEndpoint("add-order-queue", e =>
        {
            e.Bind("add_order_exchange", x =>
            {
                x.ExchangeType = ExchangeType.Fanout;
            });
            e.ConfigureConsumer<AddOrderConsumer>(context);
        });

        cfg.UseJsonSerializer();
    });
});
builder.Services.AddMassTransitHostedService();


builder.Services.AddGrpc();

var app = builder.Build();

app.MapDefaultEndpoints();

if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        var errorResponse = new ErrorResponse
        {
            Message = exception?.Message,
            StackTrace = exception?.StackTrace
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});

app.MapGrpcService<ProductServ>();
app.MapGet("/", () => "This is a gRPC server. Use a gRPC client to communicate.");

app.Run();
