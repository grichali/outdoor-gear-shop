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
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDbSettings")
    );

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.MongoDbConnectionString);
});


builder.Services.AddScoped<IProductService,ProductServ>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<ICloudinaryService,CloudinaryService>();
builder.Services.AddScoped<MongoDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(x => new Cloudinary(builder.Configuration["Cloudinary:CloudinaryUrl"]));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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

app.Run();
