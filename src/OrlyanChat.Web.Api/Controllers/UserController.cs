using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OrlyanChat.Web.Api.Requests;
using OrlyanChat.Web.Api.Services.Users;

namespace OrlyanChat.Web.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("users/")]
public sealed class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest? loginRequest)
    {
        if (loginRequest is null)
        {
            return BadRequest();
        }

        if (loginRequest.Username is null)
        {
            return BadRequest();
        }

        if (loginRequest.Password is null)
        {
            return BadRequest();
        }

        var didLogin = await userService.Login(loginRequest.Username, loginRequest.Password);

        if (didLogin is false)
        {
            return Unauthorized();
        }

        return Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest? logoutRequest)
    {
        if (logoutRequest is null)
        {
            return BadRequest();
        }

        if (logoutRequest.Username is null)
        {
            return BadRequest();
        }

        await userService.Logout(logoutRequest.Username);

        return Ok();
    }

    [HttpGet("status/{username}")]
    public async Task<IActionResult> Status([FromRoute] string? username)
    {
        if (username is null)
        {
            return BadRequest();
        }

        var status = await userService.GetUserStatus(username);

        return Ok(status);
    }
}
