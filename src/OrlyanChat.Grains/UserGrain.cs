using Orleans;
using OrlyanChat.Grains.Interfaces;
using OrlyanChat.Model;

namespace OrlyanChat.Grains;

public sealed class UserGrain : Grain, IUserGrain
{
    private UserStatus currentStatus = UserStatus.Offline;

    public Task Login()
    {
        currentStatus = UserStatus.Online;

        return Task.CompletedTask;
    }

    public Task Logout()
    {
        currentStatus = UserStatus.Offline;

        return Task.CompletedTask;
    }

    public Task<UserStatus> GetCurrentStatus()
    {
        return Task.FromResult(currentStatus);
    }
}
