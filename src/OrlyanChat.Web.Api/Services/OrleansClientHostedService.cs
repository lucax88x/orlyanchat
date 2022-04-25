using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using Orleans.Runtime.Messaging;
using OrlyanChat.Grains;

namespace OrlyanChat.Web.Api.Services;

public sealed class OrleansClientHostedService : IHostedService
{
    private const int MAX_CONNECTION_ATTEMPTS = 3;
    private static readonly TimeSpan RETRY_DELAY = TimeSpan.FromSeconds(10);

    public OrleansClientHostedService(ILoggerProvider applicationLoggerProvider)
    {
        if (applicationLoggerProvider is null)
        {
            throw new ArgumentNullException(nameof(applicationLoggerProvider));
        }

        Client = new ClientBuilder().UseLocalhostClustering(30000).Configure<ClusterOptions>(opts =>
            {
                opts.ClusterId = "local-orlyanchat-cluster";
                opts.ServiceId = "orlyanchat";
            }).ConfigureLogging(logging => { logging.AddProvider(applicationLoggerProvider); })
            .ConfigureApplicationParts(partman => { partman.AddApplicationPart(typeof(RngGrain).Assembly); }).Build();
    }

    public IClusterClient Client { get; }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var connectionAttempts = 0;

        async Task<bool> ConnectRetryLogic(Exception whatWentWrong)
        {
            if (connectionAttempts >= MAX_CONNECTION_ATTEMPTS)
            {
                return false;
            }

            if (whatWentWrong is SiloUnavailableException or ConnectionFailedException)
            {
                connectionAttempts = connectionAttempts + 1;

                await Task.Delay(RETRY_DELAY, cancellationToken);

                return true;
            }

            return false;
        }

        await Client.Connect(ConnectRetryLogic);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Client.Close();
        await Client.DisposeAsync();
    }
}
