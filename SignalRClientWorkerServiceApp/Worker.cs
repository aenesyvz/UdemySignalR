using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SignalRClientWorkerServiceApp;

public class Worker(ILogger<Worker> logger,IConfiguration configuration): BackgroundService
{

    private HubConnection? connection;
    public override Task StartAsync(CancellationToken cancellationToken)
    {

        connection = new HubConnectionBuilder().WithUrl(configuration.GetSection("SignalR")["Hub"]!).Build();

        connection?.StartAsync().ContinueWith((result) =>
        {
            logger.LogInformation(result.IsCompletedSuccessfully ? "Connected" : "Connection failed");
        });

        return base.StartAsync(cancellationToken);
    }


    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await connection!.StopAsync(cancellationToken);
        await connection!.DisposeAsync();

        base.StopAsync(cancellationToken);
    }


    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        connection!.On<Product>("ReceiveTypedMessageForAllClient",
        (product) => { logger.LogInformation($"Received message: {product.Id}-{product.Name}-{product.Price}"); });

        return Task.CompletedTask;
    }

}
