var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis")
    .WithImage("redis", tag: "latest");

var sqlserver = builder.AddSqlServer("sqlserver")
    .WithVolume("sqlserver_data", "/var/opt/mssql")
    .WithEnvironment("SA_PASSWORD", "NewPassword!2024")
    .WithEnvironment("ACCEPT_EULA", "Y");

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin(port: 15672)
    .WithEnvironment("RABBITMQ_DEFAULT_USER", "guest")
    .WithEnvironment("RABBITMQ_DEFAULT_PASS", "guest")
    .WithImage("rabbitmq", tag: "3-management");

var mongodb = builder.AddMongoDB("mongodb")
    .WithVolume("mongo_data", "/data/db");

var productService = builder.AddProject<Projects.ProductService_API>("productservice-api")
    .WithReference(rabbitmq)
    .WithReference(mongodb)
    .WithReference(redis)
    .WithEndpoint(name: "grpc", scheme: "http", port: 5001);

builder.AddProject<Projects.OrderService>("orderservice")
    .WithReference(rabbitmq)
    .WithReference(mongodb)
    .WithReference(productService);

builder.AddProject<Projects.UserService>("userservice")
    .WithReference(sqlserver);

builder.Build().Run();