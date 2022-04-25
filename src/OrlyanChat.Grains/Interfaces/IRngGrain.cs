using Orleans;

namespace OrlyanChat.Grains.Interfaces;

public interface IRngGrain : IGrainWithGuidKey
{
    Task<int> GetSomeNumber();
}
