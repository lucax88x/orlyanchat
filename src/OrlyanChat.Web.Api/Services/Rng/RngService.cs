using Orleans;
using OrlyanChat.Grains.Interfaces;

namespace OrlyanChat.Web.Api.Services.Rng;

public sealed class RngService : IRngService
{
    private readonly IClusterClient clusterClient;

    public RngService(IClusterClient clusterClient)
    {
        this.clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
    }

    public async Task<int> GetSomeNumber()
    {
        var rngGrain = clusterClient.GetGrain<IRngGrain>(Guid.Empty);

        var number = await rngGrain.GetSomeNumber();

        return number;
    }
}
