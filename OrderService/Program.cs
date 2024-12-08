using Contracts.EventContracts;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderService;
using OrderService.Interfaces;
using OrderService.Repositories;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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

/*    x.AddConsumer<FirstEventConsumer>();

    x.AddConsumer<SecondEventConsumer>();*/

    x.UsingRabbitMq((context, cfg) =>
    {

        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.Message<IAddOrderEvent>(m =>
        {
            m.SetEntityName("add_order_exchange");
        });

        cfg.ConfigureEndpoints(context);

 /*       cfg.ReceiveEndpoint("first-publish-queue", e =>
        {
            e.Bind("publish_exchange");
            e.ConfigureConsumer<FirstEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("second-publish-queue", e =>
        {
            e.Bind("publish_exchange");
            e.ConfigureConsumer<SecondEventConsumer>(context);
        });*/



    });
});

builder.Services.AddMassTransitHostedService();
var app = builder.Build();

if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
