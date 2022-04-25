using OrlyanChat.Model;

namespace OrlyanChat.Web.Api.Services.Users;

public interface IUserService
{
    Task Logout(string username);

    Task<bool> Login(string username, string password);

    Task<UserStatus> GetUserStatus(string username);
}
