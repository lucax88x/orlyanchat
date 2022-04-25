using Orleans;
using OrlyanChat.Grains.Interfaces;
using OrlyanChat.Model;

namespace OrlyanChat.Web.Api.Services.Users;

public sealed class UserService : IUserService
{
    private static readonly Dictionary<string, string> VALID_USERNAMES_AND_PASSWORDS = new()
    {
        ["pippo"] = "pippo-pwd",
        ["pluto"] = "pluto-pwd",
        ["topolino"] = "topolino-pwd"
    };

    private readonly IGrainFactory grainFactory;

    public UserService(IGrainFactory grainFactory)
    {
        this.grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
    }

    public async Task Logout(string username)
    {
        if (username is null)
        {
            throw new ArgumentNullException(nameof(username));
        }

        var userGrain = grainFactory.GetGrain<IUserGrain>(username);

        await userGrain.Logout();
    }

    public async Task<bool> Login(string username, string password)
    {
        if (username is null)
        {
            throw new ArgumentNullException(nameof(username));
        }

        if (password is null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        var didLogin = PretendToLogin(username, password);

        if (didLogin is false)
        {
            return false;
        }

        var userGrain = grainFactory.GetGrain<IUserGrain>(username);

        await userGrain.Login();

        return true;
    }

    public async Task<UserStatus> GetUserStatus(string username)
    {
        if (username is null)
        {
            throw new ArgumentNullException(nameof(username));
        }

        var userGrain = grainFactory.GetGrain<IUserGrain>(username);

        return await userGrain.GetCurrentStatus();
    }

    private static bool PretendToLogin(string username, string password)
    {
        if (username is null)
        {
            throw new ArgumentNullException(nameof(username));
        }

        if (password is null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        if (VALID_USERNAMES_AND_PASSWORDS.ContainsKey(username))
        {
            return VALID_USERNAMES_AND_PASSWORDS[username] == password;
        }

        return false;
    }
}
