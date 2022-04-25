using System;
using System.Threading.Tasks;
using FluentAssertions;
using OrlyanChat.Grains.Interfaces;
using OrlyanChat.Model;
using Xunit;

namespace OrlyanChat.Tests;

public sealed class UserGrainTest : IClassFixture<OrleansFixture>
{
    private readonly OrleansFixture fixture;

    public UserGrainTest(OrleansFixture fixture)
    {
        this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    }

    [Fact]
    public async Task User_status_is_online_when_logging_in()
    {
        var someUsername = "test-user";

        var userGrain = fixture.Factory.GetGrain<IUserGrain>(someUsername);

        await userGrain.Login();

        var userStatus = await userGrain.GetCurrentStatus();

        userStatus.Should().Be(UserStatus.Online);
    }

    [Fact]
    public async Task User_status_is_offline_after_logging_out()
    {
        var someUsername = "test-user";

        var userGrain = fixture.Factory.GetGrain<IUserGrain>(someUsername);

        await userGrain.Login();

        var userStatusAfterLogin = await userGrain.GetCurrentStatus();

        userStatusAfterLogin.Should().Be(UserStatus.Online);

        await userGrain.Logout();

        var userStatusAfterLogout = await userGrain.GetCurrentStatus();

        userStatusAfterLogout.Should().Be(UserStatus.Offline);
    }
}
