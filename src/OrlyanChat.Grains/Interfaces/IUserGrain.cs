using Orleans;
using OrlyanChat.Model;

namespace OrlyanChat.Grains.Interfaces;

public interface IUserGrain : IGrainWithStringKey
{
    Task Login();

    Task Logout();

    Task<UserStatus> GetCurrentStatus();
}
