using System.Text.Json.Serialization;
using Contracts.EventContracts;
using GrpcOrderToProduct;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderService;
using OrderService.Interfaces;
using OrderService.Repositories;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.WriteIndented = true;       
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); 
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.IncludeFields = true;
}); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderServ>();
builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDbSettings")
);

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.MongoDbConnectionString);
});

builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});
builder.Services.AddMassTransit(x =>
{

    x.UsingRabbitMq((context, cfg) =>
    {

        cfg.Host("127.0.0.1", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.UseMessageRetry(r => 
        {
            r.Interval(5, TimeSpan.FromSeconds(10));
        });

        cfg.Message<IAddOrderEvent>(m =>
        {
            m.SetEntityName("add_order_exchange");
        }); 
    
        cfg.ConfigureEndpoints(context);

    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddGrpcClient<GrpcProductService.GrpcProductServiceClient>(options =>
{
    options.Address = new Uri("http://productservice-api");
});

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

app.Run();
