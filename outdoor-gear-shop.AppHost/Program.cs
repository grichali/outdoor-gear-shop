var builder = DistributedApplication.CreateBuilder(args);


var redis = builder.AddRedis("redis", port: 6379)
    .WithImage("redis", tag: "latest");

var sqlserver = builder.AddSqlServer("sqlserver", port: 1433)
    .WithVolume("sqlserver_data", "/var/opt/mssql")
    .WithEnvironment("SA_PASSWORD", "NewPassword!2024")
    .WithEnvironment("ACCEPT_EULA", "Y");

var rabbitmq = builder.AddRabbitMQ("rabbitmq", port: 5672)
    .WithManagementPlugin(port: 15672)
    .WithEnvironment("RABBITMQ_DEFAULT_USER", "guest")
    .WithEnvironment("RABBITMQ_DEFAULT_PASS", "guest")
    .WithImage("rabbitmq", tag: "3-management");
var monogdb = builder.AddMongoDB("mongodb", port: 27017)
    .WithVolume("mongo_data", "/data/db");

builder.AddProject<Projects.OrderService>("orderservice")
    .WithReference(rabbitmq)
    .WithReference(monogdb);

builder.AddProject<Projects.ProductService_API>("productservice-api")
    .WithReference(rabbitmq)
    .WithReference(monogdb)
    .WithReference(redis);

builder.AddProject<Projects.UserService>("userservice")
    .WithReference(sqlserver);


builder.Build().Run();
