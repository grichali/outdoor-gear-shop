using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductService.Infrastructure;
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

builder.Services.AddSingleton(x => new Cloudinary(builder.Configuration["Cloudinary:CloudinaryUrl"]));
builder.Services.AddScoped(ICloudinary , CloudinaryService)

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
