using Orleans;
using Orleans.Concurrency;
using OrlyanChat.Grains.Interfaces;

namespace OrlyanChat.Grains;

[StatelessWorker]
public sealed class RngGrain : Grain, IRngGrain
{
    private readonly Random rng = new(0xB00B);

    public Task<int> GetSomeNumber()
    {
        var number = rng.Next();

        return Task.FromResult(number);
    }
}
