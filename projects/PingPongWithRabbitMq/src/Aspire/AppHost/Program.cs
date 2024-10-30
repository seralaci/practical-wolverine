var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("rabbitmq-username", secret: true);
var password = builder.AddParameter("rabbitmq-password", secret: true);

var rabbitmq = builder
    .AddRabbitMQ("rabbit", username, password)
    .WithManagementPlugin()
    .WithHealthCheck();

builder
    .AddProject<Projects.Pinger>("pinger")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

builder
    .AddProject<Projects.Ponger>("ponger")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

await builder.Build().RunAsync();
